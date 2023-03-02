using BusinessLogic.Mappers;
using Api.Handlers.Interface;
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

    public async Task<ServiceResponse<NodeDto>> GetNode(string name, Guid node)
    {
        var nodeObject = await nodeRepository.GetNode(name, node);
        if (nodeObject is null) return ServiceResponse<NodeDto>.NotOk("node not found");

        return new ServiceResponse<NodeDto>
        {
            Data = NodeMapper.NodeToDto(nodeObject),
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

    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<Guid> guids)
    {
        var nodes = await nodeRepository.GetNodes(name, guids);
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

    public async Task<ServiceResponse<bool>> EditNodePosition(string nodesetName, Guid node, VectorDto position)
    {
        var positionParsed = TryParseVector(position, out Vector? validPosition);
        if (!positionParsed) return ServiceResponse<bool>.NotOk("failed to parse position");

        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.EditNodePosition(nodesetName, node, validPosition!)
        };
    }

    public async Task<ServiceResponse<bool>> EditNodeLinks(string nodesetName, Guid node, IEnumerable<Guid> links)
    {
        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.EditNodeLinks(nodesetName, node, links)
        };
    }

    public async Task<ServiceResponse<bool>> EditNode(string nodesetName, NodeDto nodeDto)
    {
        var positionParsed = TryParseVector(nodeDto.Position, out Vector? validPosition);
        if (!positionParsed) return ServiceResponse<bool>.NotOk("failed to parse position");

        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.EditNode(nodesetName, nodeDto.Id, validPosition!, nodeDto.Links)
        };
    }

    public async Task<ServiceResponse<bool>> DeleteNode(string nodesetName, Guid node)
    {
        return new ServiceResponse<bool>
        {
            Data = await nodeRepository.DeleteNode(nodesetName, node)
        };
    }

    public async Task<ServiceResponse<int>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        return new ServiceResponse<int>
        {
            Data = await nodeRepository.DeleteNodes(nodesetName, guids)
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

    private static bool TryParseVector(VectorDto vectorDto, out Vector? vector)
    {
        vector = default;

        try
        {
            vector = NodeMapper.DtoToVector(vectorDto);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}