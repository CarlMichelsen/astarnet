using Api.Handlers.Interface;
using BusinessLogic.Mappers;
using Database.Repositories.Interface;
using Dto.Models;
using Dto;

namespace Api.Handlers;

public class NodesetHandler : INodesetHandler
{
    private readonly INodesetRepository nodesetRepository;

    public NodesetHandler(INodesetRepository nodesetRepository)
    {
        this.nodesetRepository = nodesetRepository;
    }

    public async Task<ServiceResponse<bool>> CreateNodeset(string name)
    {
        return new ServiceResponse<bool>
        {
            Data = await nodesetRepository.CreateNodeset(name),
        };
    }

    public async Task<ServiceResponse<bool>> DeleteNodeset(string name)
    {
        return new ServiceResponse<bool>
        {
            Data = await nodesetRepository.DeleteNodeset(name),
        };
    }

    public async Task<ServiceResponse<bool>> EditNodeset(string name, string newName)
    {
        return new ServiceResponse<bool>
        {
            Data = await nodesetRepository.EditNodeset(name, newName),
        };
    }

    public async Task<ServiceResponse<IEnumerable<NodesetDto>>> GetAllNodesets()
    {
        var nodesets = await nodesetRepository.GetAllNodesets();
        return new ServiceResponse<IEnumerable<NodesetDto>>
        {
            Data = nodesets.Select(NodeMapper.NodesetToDto),
        };
    }

    public async Task<ServiceResponse<NodesetDto>> GetNodeset(string name)
    {
        var nodeset = await nodesetRepository.GetNodeset(name);
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