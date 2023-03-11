using Astar.Database.Entities;

namespace Astar.Database.Repositories.Interface;

/// <summary>
/// Nodeset CRUD repository.
/// </summary>
public interface INodesetRepository
{
    /// <summary>
    /// Create a new nodeset.
    /// </summary>
    /// <param name="name">Name of the new nodeset.</param>
    /// <returns>True if nodeset was created.</returns>
    Task<bool> CreateNodeset(string name);

    /// <summary>
    /// Edit a nodeset. Only name edit for now.
    /// </summary>
    /// <param name="name">Name of the nodeset to edit.</param>
    /// <param name="newName">New name of the nodeset.</param>
    /// <returns>True if edit was succesful.</returns>
    Task<bool> EditNodeset(string name, string newName);

    /// <summary>
    /// Delete a nodeset.
    /// </summary>
    /// <param name="name">Name of nodeset to delete.</param>
    /// <returns>True if nodeset was succesfully deleted.</returns>
    Task<bool> DeleteNodeset(string name);

    /// <summary>
    /// Get nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset to get.</param>
    /// <returns>Nodeset if exsists.</returns>
    Task<Nodeset?> GetNodeset(string name);

    /// <summary>
    /// Get a list of exsisting nodeset names.
    /// </summary>
    /// <returns>List of nodeset names.</returns>
    Task<IEnumerable<string>> GetAllNodesets();
}