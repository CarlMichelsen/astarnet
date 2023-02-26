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
    private readonly ILogger<NodesetController> logger;
    private readonly INodesetHandler nodesetHandler;
    public NodesetController(ILogger<NodesetController> logger, INodesetHandler nodesetHandler)
    {
        this.logger = logger;
        this.nodesetHandler = nodesetHandler;
    }

    [HttpPost("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<bool>>> CreateNodeset([FromRoute] string name)
    {
        logger.LogInformation("POST nodeset <{}>", name);
        return await nodesetHandler.CreateNodeset(name);
    }

    [HttpGet("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<NodesetDto>>> GetNodeset([FromRoute] string name)
    {
        logger.LogInformation("GET nodeset <{}>", name);
        return await nodesetHandler.GetNodeset(name);
    }

    [HttpGet()]
    public async Task<ActionResult<ServiceResponse<IEnumerable<NodesetDto>>>> GetAllNodesets()
    {
        logger.LogInformation("GET all nodesets");
        return await nodesetHandler.GetAllNodesets();
    }

    [HttpPut("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<bool>>> PutNodeset([FromRoute] string name, [FromBody] string newName)
    {
        logger.LogInformation("PUT nodeset <{}>", name);
        return await nodesetHandler.EditNodeset(name, newName);
    }

    [HttpDelete("/api/v1/[controller]/{name}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteNodeset([FromRoute] string name)
    {
        logger.LogInformation("DELETE nodeset <{}>", name);
        return await nodesetHandler.DeleteNodeset(name);
    }
}