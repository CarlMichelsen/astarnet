using Dto;
using Dto.Models;

namespace Api.Handlers.Interface;

public interface INodesetHandler
{
    Task<ServiceResponse<bool>> CreateNodeset(string name);
    Task<ServiceResponse<NodesetDto>> GetNodeset(string name);
    Task<ServiceResponse<IEnumerable<NodesetDto>>> GetAllNodesets();
    Task<ServiceResponse<bool>> EditNodeset(string name, string newName);
    Task<ServiceResponse<bool>> DeleteNodeset(string name);

}