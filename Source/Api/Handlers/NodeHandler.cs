using Astar.Api.Handlers.Interface;
using Astar.BusinessLogic.Mappers;
using Astar.Database.Entities;
using Astar.Database.Repositories.Interface;
using Astar.Dto;
using Astar.Dto.Models;

namespace Astar.Api.Handlers;

/// <summary>
/// A Handler for manipulating node data.
/// </summary>
public class NodeHandler : INodeHandler
{
    private readonly INodeRepository nodeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeHandler"/> class.
    /// </summary>
    /// <param name="nodeRepository">A repository for manipulating node data.</param>
    public NodeHandler(INodeRepository nodeRepository)
    {
        this.nodeRepository = nodeRepository;
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<NodeDto>> GetNode(string name, Guid node)
    {
        var nodeObject = await this.nodeRepository.GetNode(name, node);
        if (nodeObject is null)
        {
            return ServiceResponse<NodeDto>.NotOk("node not found");
        }

        return new ServiceResponse<NodeDto>
        {
            Data = NodeMapper.NodeToDto(nodeObject),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetAllNodes(string name)
    {
        var nodes = await this.nodeRepository.GetAllNodes(name);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<Guid> guids)
    {
        var nodes = await this.nodeRepository.GetNodes(name, guids);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> CreateNodes(string nodesetName, IEnumerable<NodeDto> nodeDtos)
    {
        var parsed = TryParseNodes(nodeDtos, out IEnumerable<Node> nodesToCreate);
        if (!parsed)
        {
            return ServiceResponse<IEnumerable<NodeDto>>.NotOk("failed to parse one or more nodes");
        }

        var nodes = await this.nodeRepository.CreateNodes(nodesetName, nodesToCreate);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<bool>> EditNodePosition(string nodesetName, Guid guid, VectorDto position)
    {
        var positionParsed = TryParseVector(position, out Vector? validPosition);
        if (!positionParsed)
        {
            return ServiceResponse<bool>.NotOk("failed to parse position");
        }

        return new ServiceResponse<bool>
        {
            Data = await this.nodeRepository.EditNodePosition(nodesetName, guid, validPosition!),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<bool>> EditNodeLinks(string nodesetName, Guid guid, IEnumerable<Guid> links)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodeRepository.EditNodeLinks(nodesetName, guid, links),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<bool>> EditNode(string nodesetName, NodeDto nodeDto)
    {
        var positionParsed = TryParseVector(nodeDto.Position, out Vector? validPosition);
        if (!positionParsed)
        {
            return ServiceResponse<bool>.NotOk("failed to parse position");
        }

        return new ServiceResponse<bool>
        {
            Data = await this.nodeRepository.EditNode(nodesetName, nodeDto.Id, validPosition!, nodeDto.Links ?? new List<Guid>()),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<bool>> DeleteNode(string nodesetName, Guid guid)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodeRepository.DeleteNode(nodesetName, guid),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<int>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        return new ServiceResponse<int>
        {
            Data = await this.nodeRepository.DeleteNodes(nodesetName, guids),
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