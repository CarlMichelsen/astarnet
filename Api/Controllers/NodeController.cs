using Dto;
using Dto.Models;
using Api.Handlers.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[ServiceFilter(typeof(ExceptionFilter))]
public class NodeController : ControllerBase
{
    private readonly INodeHandler nodeHandler;
    public NodeController(INodeHandler nodeHandler)
    {
        this.nodeHandler = nodeHandler;
    }

    [HttpGet("/api/v1/[controller]/{nodeset}/{guid}")]
    public async Task<ActionResult<ServiceResponse<NodeDto>>> GetNode([FromRoute] string nodeset, [FromRoute] string guid)
    {
        var nodeResponse = await nodeHandler.GetNode(nodeset, guid);
        if (!nodeResponse.Ok) return NotFound(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpGet("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> GetNodes([FromRoute] string nodeset, [FromBody] IEnumerable<string> guids)
    {
        var nodeResponse = await nodeHandler.GetNodes(nodeset, guids);
        if (!nodeResponse.Ok) return NotFound(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpGet("/api/v1/[controller]/{nodeset}/all")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> GetAllNodes([FromRoute] string nodeset)
    {
        var nodeResponse = await nodeHandler.GetAllNodes(nodeset);
        if (!nodeResponse.Ok) return NotFound(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpPost("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> CreateNodes([FromRoute] string nodeset, [FromBody] IEnumerable<NodeDto> newNodes)
    {
        var nodeResponse = await nodeHandler.CreateNodes(nodeset, newNodes);
        if (!nodeResponse.Ok) return BadRequest(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpDelete("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteNodes([FromRoute] string nodeset, [FromBody] IEnumerable<string> guids)
    {
        var nodeResponse = await nodeHandler.DeleteNodes(nodeset, guids);
        if (!nodeResponse.Ok) return BadRequest(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpDelete("/api/v1/[controller]/{nodeset}/{guid}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromRoute] string guid)
    {
        var nodeResponse = await nodeHandler.DeleteNode(nodeset, guid);
        if (!nodeResponse.Ok) return NotFound(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpPut("/api/v1/[controller]/{nodeset}/{guid}/position")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromRoute] string guid, [FromBody] IEnumerable<float> position)
    {
        var nodeResponse = await nodeHandler.EditNodePosition(nodeset, guid, position);
        if (!nodeResponse.Ok) return NotFound(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpPut("/api/v1/[controller]/{nodeset}/{guid}/links")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromRoute] string guid, [FromBody] IEnumerable<string> links)
    {
        var nodeResponse = await nodeHandler.EditNodeLinks(nodeset, guid, links);
        if (!nodeResponse.Ok) return NotFound(nodeResponse);

        return Ok(nodeResponse);
    }

    [HttpPut("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromBody] NodeDto nodeDto)
    {
        var nodeResponse = await nodeHandler.EditNode(nodeset, nodeDto);
        if (!nodeResponse.Ok) return NotFound(nodeResponse);

        return Ok(nodeResponse);
    }
}