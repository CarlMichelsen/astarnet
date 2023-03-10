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

    /// <inheritdoc />
    public async Task<ServiceResponse<bool>> CreateNodeset(string name)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodesetRepository.CreateNodeset(name),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<bool>> DeleteNodeset(string name)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodesetRepository.DeleteNodeset(name),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<bool>> EditNodeset(string name, string newName)
    {
        return new ServiceResponse<bool>
        {
            Data = await this.nodesetRepository.EditNodeset(name, newName),
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IEnumerable<NodesetDto>>> GetAllNodesets()
    {
        var nodesets = await this.nodesetRepository.GetAllNodesets();
        return new ServiceResponse<IEnumerable<NodesetDto>>
        {
            Data = nodesets.Select(NodeMapper.NodesetToDto),
        };
    }

    /// <inheritdoc />
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