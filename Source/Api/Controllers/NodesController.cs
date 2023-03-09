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
    /// Get a single node.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to get the node from.</param>
    /// <param name="node">A Guid to identify the node you want to fetch.</param>
    /// <returns>A ServiceResponse that contains a NodeDto if the request is successful.</returns>
    [HttpGet("{nodeset}/{node}")]
    public async Task<ActionResult<ServiceResponse<NodeDto>>> GetNode([FromRoute] string nodeset, [FromRoute] Guid node)
    {
        var nodeResponse = await this.nodeHandler.GetNode(nodeset, node);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.NotFound(nodeResponse);
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
    /// Get all nodes.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to get the nodes from.</param>
    /// <returns>A ServiceResponse that contains a List of NodeDto if the request is successful.</returns>
    [HttpGet("{nodeset}/all")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodeDto>>>> GetAllNodes([FromRoute] string nodeset)
    {
        var nodeResponse = await this.nodeHandler.GetAllNodes(nodeset);
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
    public async Task<ActionResult<ServiceResponse<int>>> DeleteNodes([FromRoute] string nodeset, [FromBody] IEnumerable<Guid> guids)
    {
        var nodeResponse = await this.nodeHandler.DeleteNodes(nodeset, guids);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.BadRequest(nodeResponse);
    }

    /// <summary>
    /// Delete a node.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to delete the node from.</param>
    /// <param name="node">A guid that identifies the node you want to delete.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the deletion is successful.</returns>
    [HttpDelete("{nodeset}/{node}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNode([FromRoute] string nodeset, [FromRoute] Guid node)
    {
        var nodeResponse = await this.nodeHandler.DeleteNode(nodeset, node);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.NotFound(nodeResponse);
    }

    /// <summary>
    /// Edit the position of a node.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="node">A Guid that identifies the node you want to edit.</param>
    /// <param name="position">A VectorDto that defines the new position of the given node.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit was successful.</returns>
    [HttpPut("{nodeset}/{node}/position")]
    public async Task<ActionResult<ServiceResponse<bool>>> EditNode([FromRoute] string nodeset, [FromRoute] Guid node, [FromBody] VectorDto position)
    {
        var nodeResponse = await this.nodeHandler.EditNodePosition(nodeset, node, position);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.NotFound(nodeResponse);
    }

    /// <summary>
    /// Edit the links of a node.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="node">A Guid that identifies the node you want to edit.</param>
    /// <param name="links">A List of Guids that overwrites the exsisting links of the node you want to edit.</param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit was successful.</returns>
    [HttpPut("{nodeset}/{node}/links")]
    public async Task<ActionResult<ServiceResponse<bool>>> EditNode([FromRoute] string nodeset, [FromRoute] Guid node, [FromBody] IEnumerable<Guid> links)
    {
        var nodeResponse = await this.nodeHandler.EditNodeLinks(nodeset, node, links);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.NotFound(nodeResponse);
    }

    /// <summary>
    /// Edit a node.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset that contains the node you want to edit.</param>
    /// <param name="nodeDto">
    /// A NodeDto that needs to have a Guid in the Id field to identify the target node.
    /// </param>
    /// <returns>A ServiceResponse that contains a boolean that is true if the edit was successful.</returns>
    [HttpPut("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<bool>>> EditNode([FromRoute] string nodeset, [FromBody] NodeDto nodeDto)
    {
        var nodeResponse = await this.nodeHandler.EditNode(nodeset, nodeDto);
        return nodeResponse.Ok ? this.Ok(nodeResponse) : this.NotFound(nodeResponse);
    }
}