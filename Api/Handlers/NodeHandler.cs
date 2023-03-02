using BusinessLogic.Mappers;
using Api.Handlers.Interface;
using Api.Extensions;
using Database.Repositories.Interface;
using Database.Entities;
using Dto;
using Dto.Models;


namespace Api.Handlers;

public class NodeHandler : INodeHandler
{
    private readonly INodeRepository nodeRepository;

    public NodeHandler(INodeRepository nodeRepository)
    {
        this.nodeRepository = nodeRepository;
    }

    public async Task<ServiceResponse<NodeDto>> GetNode(string name, string guid)
    {
        var parsed = Guid.TryParse(guid, out Guid validGuid);
        if (!parsed) return ServiceResponse<NodeDto>.NotOk("failed to parse guid");

        var node = await nodeRepository.GetNode(name, validGuid);
        if (node is null) return ServiceResponse<NodeDto>.NotOk("node not found");

        return new ServiceResponse<NodeDto>
        {
            Data = NodeMapper.NodeToDto(node),
        };
    }

    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetAllNodes(string name)
    {
        var nodes = await nodeRepository.GetAllNodes(name);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<string> guids)
    {
        var parsed = guids.TryParseGuid(out IEnumerable<Guid> validGuids);
        if (!parsed) return ServiceResponse<IEnumerable<NodeDto>>.NotOk("failed to parse one or more guids");

        var nodes = await nodeRepository.GetNodes(name, validGuids);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    public async Task<ServiceResponse<IEnumerable<NodeDto>>> CreateNodes(string nodesetName, IEnumerable<NodeDto> nodeDtos)
    {
        var parsed = TryParseNodes(nodeDtos, out IEnumerable<Node> nodesToCreate);
        if (!parsed) return ServiceResponse<IEnumerable<NodeDto>>.NotOk("failed to parse one or more nodes");

        var nodes = await nodeRepository.CreateNodes(nodesetName, nodesToCreate);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    public async Task<ServiceResponse<bool>> EditNodePosition(string nodesetName, string guid, IEnumerable<float> position)
    {
        var isGuidParsed = Guid.TryParse(guid, out Guid validGuid);
        if (!isGuidParsed) return ServiceResponse<bool>.NotOk("failed to parse guid");

        var positionParsed = TryParseVector(position, out Vector? validPosition);
        if (!positionParsed) return ServiceResponse<bool>.NotOk("failed to parse position");

        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.EditNodePosition(nodesetName, validGuid, validPosition!)
        };
    }

    public async Task<ServiceResponse<bool>> EditNodeLinks(string nodesetName, string guid, IEnumerable<string> links)
    {
        var isGuidParsed = Guid.TryParse(guid, out Guid validGuid);
        if (!isGuidParsed) return ServiceResponse<bool>.NotOk("failed to parse guid");

        var guidsAreParsed = links.TryParseGuid(out IEnumerable<Guid> validLinkGuids);
        if (!guidsAreParsed) return ServiceResponse<bool>.NotOk("failed to parse one or more link guids");

        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.EditNodeLinks(nodesetName, validGuid, validLinkGuids)
        };
    }

    public async Task<ServiceResponse<bool>> EditNode(string nodesetName, NodeDto nodeDto)
    {
        var isGuidParsed = Guid.TryParse(nodeDto.Id, out Guid validGuid);
        if (!isGuidParsed) return ServiceResponse<bool>.NotOk("failed to parse id guid");

        var guidsAreParsed = nodeDto.Links.TryParseGuid(out IEnumerable<Guid> validLinkGuids);
        if (!guidsAreParsed) return ServiceResponse<bool>.NotOk("failed to parse one or more link guids");

        var positionParsed = TryParseVector(nodeDto.Position, out Vector? validPosition);
        if (!positionParsed) return ServiceResponse<bool>.NotOk("failed to parse position");

        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.EditNode(nodesetName, validGuid, validPosition!, validLinkGuids)
        };
    }

    public async Task<ServiceResponse<bool>> DeleteNode(string nodesetName, string guid)
    {
        var isGuidParsed = Guid.TryParse(guid, out Guid validGuid);
        if (!isGuidParsed) return ServiceResponse<bool>.NotOk("failed to parse guid");

        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.DeleteNode(nodesetName, validGuid)
        };
    }

    public async Task<ServiceResponse<int>> DeleteNodes(string nodesetName, IEnumerable<string> guids)
    {
        var guidsAreParsed = guids.TryParseGuid(out IEnumerable<Guid> validLinkGuids);
        if (!guidsAreParsed) return ServiceResponse<int>.NotOk("failed to parse one or more guids");

        return new ServiceResponse<int>
        {
            Data = await nodeRepository.DeleteNodes(nodesetName, validLinkGuids)
        };
    }

    private static bool TryParseNodes(IEnumerable<NodeDto> dtos, out IEnumerable<Node> parsedNodes)
    {
        parsedNodes = new List<Node>();
        try
        {
            parsedNodes = dtos.Select(NodeMapper.DtoToNode).ToList();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool TryParseVector(IEnumerable<float> vectorDto, out Vector? vector)
    {
        vector = default;

        try
        {
            vector = NodeMapper.DtoToVector(vectorDto.ToList());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}