using Astar.Database.Entities;

namespace Astar.Database.Repositories.Interface;

/// <summary>
/// Repository that manipulates nodes in nodesets.
/// </summary>
public interface INodeRepository
{
    /// <summary>
    /// Create nodes in a nodeset while maintaining link data-integrity.
    /// All links are two-way.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset to add nodes in.</param>
    /// <param name="nodes">Nodes to add.</param>
    /// <returns>List of added nodes.</returns>
    Task<IEnumerable<Node>> CreateNodes(string nodesetName, IEnumerable<Node> nodes);

    /// <summary>
    /// Edit multiple nodes while maintaining link data-integrity.
    /// All links are two-way.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset to edit nodes in.</param>
    /// <param name="nodes">Nodes to edit using the Id in the node as identifier.</param>
    /// <returns>List of edited nodes.</returns>
    Task<IEnumerable<Node>> EditNodes(string nodesetName, IEnumerable<Node> nodes);

    /// <summary>
    /// Get nodes from a nodeset while maintaining link data-integrity.
    /// All links are two-way.
    /// </summary>
    /// <param name="nodesetName">Name of the nodeset to read nodes from.</param>
    /// <param name="guids">List of guid node-identifiers.</param>
    /// <returns>List of nodes.</returns>
    Task<IEnumerable<Node>> GetNodes(string nodesetName, IEnumerable<Guid> guids);

    /// <summary>
    /// Delete nodes from a nodeset while maintaining link data-integrity.
    /// All links are two-way.
    /// </summary>
    /// <param name="nodesetName">Name of nodeset to delete from.</param>
    /// <param name="guids">List of guid node-identifiers to delete.</param>
    /// <returns>List of deleted nodes.</returns>
    Task<IEnumerable<Node>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids);
}