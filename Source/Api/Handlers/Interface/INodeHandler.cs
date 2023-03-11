using Astar.Dto;
using Astar.Dto.Models;

namespace Astar.Api.Handlers.Interface;

/// <summary>
/// An interface for handling nodes.
/// This is used as an additional layer of abstraction before repositories are directly accessed.
/// </summary>
public interface INodeHandler
{
    /// <summary>
    /// Get specific nodes.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get the nodes from.</param>
    /// <param name="guids">List of Guid that identifies the nodes you want to get.</param>
    /// <returns>A ServiceResponse with a list of NodeDto if the request if succesful.</returns>
    Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<Guid> guids);

    /// <summary>
    /// Create new nodes.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset you want to create the nodes in.</param>
    /// <param name="nodeDtos">List of new nodes that don't require the Id property to be set.</param>
    /// <returns>A ServiceResponse with a list of NodeDto made from the newly created nodes.</returns>
    Task<ServiceResponse<IEnumerable<NodeDto>>> CreateNodes(string nodesetName, IEnumerable<NodeDto> nodeDtos);

    /// <summary>
    /// Edit the links of a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="nodeDto">NodeDto that overwrites exsisting node. Make sure the Id is set so the node to edit can be identified.</param>
    /// <returns>A ServiceResponse that contains a list of succesfully edited nodes.</returns>
    Task<ServiceResponse<IEnumerable<NodeDto>>> EditNodes(string nodesetName, IEnumerable<NodeDto> nodeDto);

    /// <summary>
    /// Delete nodes.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the nodes you want to delete.</param>
    /// <param name="guid">Identifier for the nodes you want to delete.</param>
    /// <returns>A ServiceResponse that contains a list of deleted nodes.</returns>
    Task<ServiceResponse<IEnumerable<NodeDto>>> DeleteNodes(string nodesetName, IEnumerable<Guid> guid);
}