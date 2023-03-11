using Astar.Api.Handlers.Interface;
using Astar.Dto;
using Astar.Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Astar.Api.Controllers;

/// <summary>
/// A RESTful CRUD controller for manipulating nodes in nodesets.
/// </summary>
[ApiController]
[Route("/api/v1/[controller]")]
public class NodesController : ControllerBase
{
    private readonly INodeHandler nodeHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="NodesController"/> class.
    /// </summary>
    /// <param name="nodeHandler">A handler for manipulating node data.</param>
    public NodesController(INodeHandler nodeHandler)
    {
        this.nodeHandler = nodeHandler;
    }

    /// <summary>
    /// Get multiple nodes.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to get the nodes from.</param>
    /// <param name="guids">List of Guid that identifies the nodes you want to get.</param>
    /// <returns>A ServiceResponse that contains a List of NodeDto if the request is successful.</returns>
    [HttpGet("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> GetNodes([FromRoute] string nodeset, [FromBody] IEnumerable<Guid> guids)
    {
        var nodeResponse = await this.nodeHandler.GetNodes(nodeset, guids);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.NotFound(nodeResponse);
    }

    /// <summary>
    /// Create new nodes.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to create the nodes in.</param>
    /// <param name="newNodes">List of NodeDto that don't have to have their Id defined.</param>
    /// <returns>A ServiceResponse that contains a List of NodeDto if the request is successful.</returns>
    [HttpPost("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> CreateNodes([FromRoute] string nodeset, [FromBody] IEnumerable<NodeDto> newNodes)
    {
        var nodeResponse = await this.nodeHandler.CreateNodes(nodeset, newNodes);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.BadRequest(nodeResponse);
    }

    /// <summary>
    /// Delete nodes.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to delete the nodes from.</param>
    /// <param name="guids">List of Guid that identifies the nodes you want to delete.</param>
    /// <returns>A ServiceResponse that contains the amount of deletions made if the request is successful.</returns>
    [HttpDelete("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> DeleteNodes([FromRoute] string nodeset, [FromBody] IEnumerable<Guid> guids)
    {
        var nodeResponse = await this.nodeHandler.DeleteNodes(nodeset, guids);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.BadRequest(nodeResponse);
    }

    /// <summary>
    /// Edit nodes.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset that contains the nodes you want to edit.</param>
    /// <param name="nodeDtos">
    /// List of nodeDtos to edit. *id* and *links* do not have to be filled.
    /// </param>
    /// <returns>A ServiceResponse that contains a list of succesfully edited nodes.</returns>
    [HttpPut("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<bool>>> EditNodes([FromRoute] string nodeset, [FromBody] IEnumerable<NodeDto> nodeDtos)
    {
        var nodeResponse = await this.nodeHandler.EditNodes(nodeset, nodeDtos);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.NotFound(nodeResponse);
    }
}