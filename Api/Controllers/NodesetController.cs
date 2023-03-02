using Api.Handlers.Interface;
using Dto;
using Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[ServiceFilter(typeof(ExceptionFilter))]
public class NodesetController : ControllerBase
{
    private readonly INodesetHandler nodesetHandler;
    public NodesetController(INodesetHandler nodesetHandler)
    {
        this.nodesetHandler = nodesetHandler;
    }

    [HttpPost("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<bool>>> CreateNodeset([FromRoute] string name)
    {
        var nodesetResponse = await nodesetHandler.CreateNodeset(name);
        return nodesetResponse.Ok ? Ok(nodesetResponse) : BadRequest(nodesetResponse);
    }

    [HttpGet("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<NodesetDto>>> GetNodeset([FromRoute] string name)
    {
        var nodesetResponse = await nodesetHandler.GetNodeset(name);
        return nodesetResponse.Ok ? Ok(nodesetResponse) : NotFound(nodesetResponse);
    }

    [HttpGet()]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodesetDto>>>> GetAllNodesets()
    {
        var nodesetResponse = await nodesetHandler.GetAllNodesets();
        return Ok(nodesetResponse);
    }

    [HttpPut("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<bool>>> PutNodeset([FromRoute] string name, [FromBody] string newName)
    {
        var nodesetResponse = await nodesetHandler.EditNodeset(name, newName);
        return Ok(nodesetResponse);
    }

    [HttpDelete("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNodeset([FromRoute] string name)
    {
        var nodesetResponse = await nodesetHandler.DeleteNodeset(name);
        return Ok(nodesetResponse);
    }
}