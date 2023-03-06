using Dto;
using Dto.Models;

namespace Api.Handlers.Interface;

/// <summary>
/// An interface for handling nodesets.
/// This is used as an additional layer of abstraction before repositories are directly accessed.
/// </summary>
public interface INodesetHandler
{
    /// <summary>
    /// Create a brand new nodeset.
    /// </summary>
    /// <param name="name">Name of the new nodeset.</param>
    /// <returns>A Service response that contains a boolean that is true if the request was succesful.</returns>
    Task<ServiceResponse<bool>> CreateNodeset(string name);

    /// <summary>
    /// Get a nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get.</param>
    /// <returns>A ServiceResponse that contains a NodesetDto if the request was succesful.</returns>
    Task<ServiceResponse<NodesetDto>> GetNodeset(string name);

    /// <summary>
    /// Get ALL nodesets.
    /// </summary>
    /// <returns>A ServiceResponse that contains a list of NodesetDto if the request was succesful.</returns>
    Task<ServiceResponse<IEnumerable<NodesetDto>>> GetAllNodesets();

    /// <summary>
    /// Edit a nodeset (only name editing for now).
    /// </summary>
    /// <param name="name">Name of the nodeset you want to edit.</param>
    /// <param name="newName">New name for the nodeset.</param>
    /// <returns>A Service response that contains a boolean that is true if the request was succesful.</returns>
    Task<ServiceResponse<bool>> EditNodeset(string name, string newName);

    /// <summary>
    /// Delete a nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to delete.</param>
    /// <returns>A Service response that contains a boolean that is true if the request was succesful.</returns>
    Task<ServiceResponse<bool>> DeleteNodeset(string name);
}