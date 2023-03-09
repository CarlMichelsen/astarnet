using Astar.Api.Handlers.Interface;
using Astar.Dto;
using Astar.Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Astar.Api.Controllers;

/// <summary>
/// A RESTful CRUD controller for manipulating nodesets.
/// </summary>
[ApiController]
[Route("/api/v1/[controller]")]
public class NodesetsController : ControllerBase
{
    private readonly INodesetHandler nodesetHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="NodesetsController"/> class.
    /// </summary>
    /// <param name="nodesetHandler">A handler for manipulating nodeset data.</param>
    public NodesetsController(INodesetHandler nodesetHandler)
    {
        this.nodesetHandler = nodesetHandler;
    }

    /// <summary>
    /// Create a brand new nodeset.
    /// </summary>
    /// <param name="nodeset">Name of the brand new nodeset.</param>
    /// <returns>A ServiceResponse with a bool that indicates if the creation was succesful.</returns>
    [HttpPost("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<bool>>> CreateNodeset([FromRoute] string nodeset)
    {
        var nodesetResponse = await this.nodesetHandler.CreateNodeset(nodeset);
        return nodesetResponse.Ok ? this.Ok(nodesetResponse) : this.BadRequest(nodesetResponse);
    }

    /// <summary>
    /// Get a nodeset.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to get.</param>
    /// <returns>A ServiceResponse with a NodesetDto if request was succesful.</returns>
    [HttpGet("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<NodesetDto>>> GetNodeset([FromRoute] string nodeset)
    {
        var nodesetResponse = await this.nodesetHandler.GetNodeset(nodeset);
        return nodesetResponse.Ok ? this.Ok(nodesetResponse) : this.NotFound(nodesetResponse);
    }

    /// <summary>
    /// Get all nodesets.
    /// </summary>
    /// <returns>A ServiceResponse with a list of all nodesets.</returns>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodesetDto>>>> GetAllNodesets()
    {
        var nodesetResponse = await this.nodesetHandler.GetAllNodesets();
        return this.Ok(nodesetResponse);
    }

    /// <summary>
    /// Edit a nodeset (only name editing for now).
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to edit.</param>
    /// <param name="newName">New name of the nodeset.</param>
    /// <returns>A ServiceResponse with a boolean that is true if the edit was succesful.</returns>
    [HttpPut("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<bool>>> PutNodeset([FromRoute] string nodeset, [FromBody] string newName)
    {
        var nodesetResponse = await this.nodesetHandler.EditNodeset(nodeset, newName);
        return this.Ok(nodesetResponse);
    }

    /// <summary>
    /// Delete a nodeset.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to delete.</param>
    /// <returns>A ServiceResponse with a boolean that is true if the deletion was succesful.</returns>
    [HttpDelete("{nodeset}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNodeset([FromRoute] string nodeset)
    {
        var nodesetResponse = await this.nodesetHandler.DeleteNodeset(nodeset);
        return this.Ok(nodesetResponse);
    }
}