using Astar.Api.Handlers.Interface;
using Astar.BusinessLogic.Mappers;
using Astar.Database.Repositories.Interface;
using Astar.Dto;
using Astar.Dto.Models;

namespace Astar.Api.Handlers;

/// <summary>
/// A Handler for manipulating nodeset data.
/// </summary>
public class NodesetHandler : INodesetHandler
{
    private readonly INodesetRepository nodesetRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="NodesetHandler"/> class.
    /// </summary>
    /// <param name="nodesetRepository">A repository for manipulating nodeset data.</param>
    public NodesetHandler(INodesetRepository nodesetRepository)
    {
        this.nodesetRepository = nodesetRepository;
    }

    /// <summary>
    /// Create a brand new nodeset.
    /// </summary>
    /// <param name="name">Name of the new nodeset.</param>
    /// <returns>A Service response that contains a boolean that is true if the request was succesful.</returns>
    public async Task<ServiceResponse<bool>> CreateNodeset(string name)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodesetRepository.CreateNodeset(name),
        };
    }

    /// <summary>
    /// Delete a nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to delete.</param>
    /// <returns>A Service response that contains a boolean that is true if the request was succesful.</returns>
    public async Task<ServiceResponse<bool>> DeleteNodeset(string name)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodesetRepository.DeleteNodeset(name),
        };
    }

    /// <summary>
    /// Edit a nodeset (only name editing for now).
    /// </summary>
    /// <param name="name">Name of the nodeset you want to edit.</param>
    /// <param name="newName">New name for the nodeset.</param>
    /// <returns>A Service response that contains a boolean that is true if the request was succesful.</returns>
    public async Task<ServiceResponse<bool>> EditNodeset(string name, string newName)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodesetRepository.EditNodeset(name, newName),
        };
    }

    /// <summary>
    /// Get ALL nodesets.
    /// </summary>
    /// <returns>A ServiceResponse that contains a list of NodesetDto if the request was succesful.</returns>
    public async Task<ServiceResponse<IEnumerable<NodesetDto>>> GetAllNodesets()
    {
        var nodesets = await this.nodesetRepository.GetAllNodesets();
        return new ServiceResponse<IEnumerable<NodesetDto>>
        {
            Data = nodesets.Select(NodeMapper.NodesetToDto),
        };
    }

    /// <summary>
    /// Get a nodeset.
    /// </summary>
    /// <param name="name">Name of the nodeset you want to get.</param>
    /// <returns>A ServiceResponse that contains a NodesetDto if the request was succesful.</returns>
    public async Task<ServiceResponse<NodesetDto>> GetNodeset(string name)
    {
        var nodeset = await this.nodesetRepository.GetNodeset(name);
        if (nodeset is not null)
        {
            return new ServiceResponse<NodesetDto>
            {
                Data = NodeMapper.NodesetToDto(nodeset),
            };
        }

        return new ServiceResponse<NodesetDto>
        {
            Errors = new List<string> { "Not Found" },
        };
    }
}