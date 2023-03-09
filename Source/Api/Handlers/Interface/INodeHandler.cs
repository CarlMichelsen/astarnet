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
    /// Get a node from a nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get the node from.</param>
    /// <param name="guid">Identifies the node you want to get.</param>
    /// <returns>A ServiceResponse with a NodeDto if the request is succesful.</returns>
    Task<ServiceResponse<NodeDto>> GetNode(string name, Guid guid);

    /// <summary>
    /// Get all nodes in a nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get the nodes from.</param>
    /// <returns>A List of nodes.</returns>
    Task<ServiceResponse<IEnumerable<NodeDto>>> GetAllNodes(string name);

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
    /// Edit the position of a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="guid">Identifier for the node you want to edit.</param>
    /// <param name="position">New position for the node you want to edit.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit is succesful.</returns>
    Task<ServiceResponse<bool>> EditNodePosition(string nodesetName, Guid guid, VectorDto position);

    /// <summary>
    /// Edit the links of a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="guid">Identifier for the node you want to edit.</param>
    /// <param name="links">New links for the node you want to edit.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit is succesful.</returns>
    Task<ServiceResponse<bool>> EditNodeLinks(string nodesetName, Guid guid, IEnumerable<Guid> links);

    /// <summary>
    /// Edit the links of a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="nodeDto">NodeDto that overwrites exsisting node. Make sure the Id is set so the node to edit can be identified.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit is succesful.</returns>
    Task<ServiceResponse<bool>> EditNode(string nodesetName, NodeDto nodeDto);

    /// <summary>
    /// Delete a node.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the node you want to delete.</param>
    /// <param name="guid">Identifier for the node you want to delete.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the deletion is succesful.</returns>
    Task<ServiceResponse<bool>> DeleteNode(string nodesetName, Guid guid);

    /// <summary>
    /// Delete multiple nodes.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset that contains the nodes you want to delete.</param>
    /// <param name="guids">A list of Guid that identifies the nodes you want to delete.</param>
    /// <returns>A ServiceResponse that contains an int that defines how many deletions were succesful.</returns>
    Task<ServiceResponse<int>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids);
}