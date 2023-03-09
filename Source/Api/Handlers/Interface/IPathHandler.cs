using Astar.Dto;
using Astar.Dto.Models;

namespace Astar.Api.Handlers.Interface;

/// <summary>
/// An interface for handling path calculations.
/// </summary>
public interface IPathHandler
{
    /// <summary>
    /// Calculate a the shortest path between two nodes in the given nodeset based on settings in pathRequest.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to do the calculcations in.</param>
    /// <param name="pathRequest">Settings such as start and end-nodes for the calculation.</param>
    /// <returns>A ServiceResponse with a list of NodeDto if the request is succesful.</returns>
    public Task<ServiceResponse<IEnumerable<NodeDto>>> CalculatePath(string nodeset, PathRequestDto pathRequest);
}