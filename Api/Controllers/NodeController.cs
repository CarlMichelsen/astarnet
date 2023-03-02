using Dto;
using Dto.Models;
using Api.Handlers.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class NodeController : ControllerBase
{
    private readonly INodeHandler nodeHandler;
    public NodeController(INodeHandler nodeHandler)
    {
        this.nodeHandler = nodeHandler;
    }

    [HttpGet("/api/v1/[controller]/{nodeset}/{node}")]
    public async Task<ActionResult<ServiceResponse<NodeDto>>> GetNode([FromRoute] string nodeset, [FromRoute] Guid node)
    {
        var nodeResponse = await nodeHandler.GetNode(nodeset, node);
        return nodeResponse.Ok ? Ok(nodeResponse) : NotFound(nodeResponse);
    }

    [HttpGet("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> GetNodes([FromRoute] string nodeset, [FromBody] IEnumerable<Guid> guids)
    {
        var nodeResponse = await nodeHandler.GetNodes(nodeset, guids);
        return nodeResponse.Ok ? Ok(nodeResponse) : NotFound(nodeResponse);
    }

    [HttpGet("/api/v1/[controller]/{nodeset}/all")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> GetAllNodes([FromRoute] string nodeset)
    {
        var nodeResponse = await nodeHandler.GetAllNodes(nodeset);
        return nodeResponse.Ok ? Ok(nodeResponse) : NotFound(nodeResponse);
    }

    [HttpPost("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> CreateNodes([FromRoute] string nodeset, [FromBody] IEnumerable<NodeDto> newNodes)
    {
        var nodeResponse = await nodeHandler.CreateNodes(nodeset, newNodes);
        return nodeResponse.Ok ? Ok(nodeResponse) : BadRequest(nodeResponse);
    }

    [HttpDelete("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteNodes([FromRoute] string nodeset, [FromBody] IEnumerable<Guid> guids)
    {
        var nodeResponse = await nodeHandler.DeleteNodes(nodeset, guids);
        return nodeResponse.Ok ? Ok(nodeResponse) : BadRequest(nodeResponse);
    }

    [HttpDelete("/api/v1/[controller]/{nodeset}/{node}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromRoute] Guid node)
    {
        var nodeResponse = await nodeHandler.DeleteNode(nodeset, node);
        return nodeResponse.Ok ? Ok(nodeResponse) : NotFound(nodeResponse);
    }

    [HttpPut("/api/v1/[controller]/{nodeset}/{node}/position")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromRoute] Guid node, [FromBody] VectorDto position)
    {
        var nodeResponse = await nodeHandler.EditNodePosition(nodeset, node, position);
        return nodeResponse.Ok ? Ok(nodeResponse) : NotFound(nodeResponse);
    }

    [HttpPut("/api/v1/[controller]/{nodeset}/{node}/links")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromRoute] Guid node, [FromBody] IEnumerable<Guid> links)
    {
        var nodeResponse = await nodeHandler.EditNodeLinks(nodeset, node, links);
        return nodeResponse.Ok ? Ok(nodeResponse) : NotFound(nodeResponse);
    }

    [HttpPut("/api/v1/[controller]/{nodeset}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromBody] NodeDto nodeDto)
    {
        var nodeResponse = await nodeHandler.EditNode(nodeset, nodeDto);
        return nodeResponse.Ok ? Ok(nodeResponse) : NotFound(nodeResponse);
    }
}