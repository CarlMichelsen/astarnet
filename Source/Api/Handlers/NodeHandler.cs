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
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<Guid> guids)
    {
        var nodes = await this.nodeRepository.GetNodes(name, guids);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        var nodes = await this.nodeRepository.DeleteNodes(nodesetName, guids);
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
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> EditNodes(string nodesetName, IEnumerable<NodeDto> nodeDtos)
    {
        var parsed = TryParseNodes(nodeDtos, out IEnumerable<Node> nodesToEdit);
        if (!parsed)
        {
            return ServiceResponse<IEnumerable<NodeDto>>.NotOk("failed to parse one or more nodes");
        }

        var nodes = await this.nodeRepository.EditNodes(nodesetName, nodesToEdit);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
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
}