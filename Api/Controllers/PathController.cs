using Api.Handlers.Interface;
using Dto;
using Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PathController : ControllerBase
{
    private readonly IPathHandler pathHandler;

    public PathController(IPathHandler pathHandler)
    {
        this.pathHandler = pathHandler;
    }

    [HttpPost("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> GetNode([FromRoute] string nodeset, [FromBody] PathRequestDto pathRequest)
    {
        var nodeResponse = await pathHandler.CalculatePath(nodeset, pathRequest);
        if (!nodeResponse.Ok) return BadRequest(nodeResponse);

        return Ok(nodeResponse);
    }
}