using Api.Handlers.Interface;
using BusinessLogic.Mappers;
using Database.Entities;
using Database.Repositories.Interface;
using Dto;
using Dto.Models;

namespace Api.Handlers;

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

    /// <summary>
    /// Get a node.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get the node from.</param>
    /// <param name="node">Guid idientifer for the node you want to get.</param>
    /// <returns>A ServiceResponse that contains a NodeDto if the request was succesful.</returns>
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

    /// <summary>
    /// Get all nodes in a nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get the nodes from.</param>
    /// <returns>A List of nodes.</returns>
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetAllNodes(string name)
    {
        var nodes = await this.nodeRepository.GetAllNodes(name);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    /// <summary>
    /// Get specific nodes.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get the nodes from.</param>
    /// <param name="guids">List of Guid that identifies the nodes you want to get.</param>
    /// <returns>A ServiceResponse with a list of NodeDto if the request if succesful.</returns>
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<Guid> guids)
    {
        var nodes = await this.nodeRepository.GetNodes(name, guids);
        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = nodes.Select(NodeMapper.NodeToDto),
        };
    }

    /// <summary>
    /// Create new nodes.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset you want to create the nodes in.</param>
    /// <param name="nodeDtos">List of new nodes that don't require the Id property to be set.</param>
    /// <returns>A ServiceResponse with a list of NodeDto made from the newly created nodes.</returns>
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

    /// <summary>
    /// Edit the position of a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="guid">Identifier for the node you want to edit.</param>
    /// <param name="position">New position for the node you want to edit.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit is succesful.</returns>
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

    /// <summary>
    /// Edit the links of a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="guid">Identifier for the node you want to edit.</param>
    /// <param name="links">New links for the node you want to edit.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit is succesful.</returns>
    public async Task<ServiceResponse<bool>> EditNodeLinks(string nodesetName, Guid guid, IEnumerable<Guid> links)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodeRepository.EditNodeLinks(nodesetName, guid, links),
        };
    }

    /// <summary>
    /// Edit the links of a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="nodeDto">NodeDto that overwrites exsisting node. Make sure the Id is set so the node to edit can be identified.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit is succesful.</returns>
    public async Task<ServiceResponse<bool>> EditNode(string nodesetName, NodeDto nodeDto)
    {
        var positionParsed = TryParseVector(nodeDto.Position, out Vector? validPosition);
        if (!positionParsed)
        {
            return ServiceResponse<bool>.NotOk("failed to parse position");
        }

        return new ServiceResponse<bool>
        {
            Data = await this.nodeRepository.EditNode(nodesetName, nodeDto.Id, validPosition!, nodeDto.Links),
        };
    }

    /// <summary>
    /// Delete a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to delete.</param>
    /// <param name="guid">Identifier for the node you want to delete.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the deletion is succesful.</returns>
    public async Task<ServiceResponse<bool>> DeleteNode(string nodesetName, Guid guid)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodeRepository.DeleteNode(nodesetName, guid),
        };
    }

    /// <summary>
    /// Delete multiple nodes.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the nodes you want to delete.</param>
    /// <param name="guids">A list of Guid that identifies the nodes you want to delete.</param>
    /// <returns>A ServiceResponse that contains an int that defines how many deletions were succesful.</returns>
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