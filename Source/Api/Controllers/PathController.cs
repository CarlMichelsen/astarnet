using Api.Handlers.Interface;
using Dto;
using Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// A controller for calculating paths between nodes in nodesets.
/// </summary>
[ApiController]
[Route("/api/v1/[controller]")]
public class PathController : ControllerBase
{
    private readonly IPathHandler pathHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="PathController"/> class.
    /// </summary>
    /// <param name="pathHandler">A handler for path calculations.</param>
    public PathController(IPathHandler pathHandler)
    {
        this.pathHandler = pathHandler;
    }

    /// <summary>
    /// Calculate a path.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to calculate the path in.</param>
    /// <param name="pathRequest">Dto that contains information about the path calculation such as start-node and end-node.</param>
    /// <returns>A List of NodeDto that represents the fastest route from start to end.</returns>
    [HttpPost("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> CalculatePath([FromRoute] string nodeset, [FromBody] PathRequestDto pathRequest)
    {
        var nodeResponse = await this.pathHandler.CalculatePath(nodeset, pathRequest);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.BadRequest(nodeResponse);
    }
}