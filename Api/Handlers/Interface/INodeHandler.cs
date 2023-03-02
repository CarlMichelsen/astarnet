using Dto;
using Dto.Models;

namespace Api.Handlers.Interface;

public interface INodeHandler
{
    Task<ServiceResponse<NodeDto>> GetNode(string name, Guid guid);
    Task<ServiceResponse<IEnumerable<NodeDto>>> GetAllNodes(string name);
    Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<Guid> guids);
    Task<ServiceResponse<IEnumerable<NodeDto>>> CreateNodes(string nodesetName, IEnumerable<NodeDto> nodeDtos);
    Task<ServiceResponse<bool>> EditNodePosition(string nodesetName, Guid guid, VectorDto position);
    Task<ServiceResponse<bool>> EditNodeLinks(string nodesetName, Guid guid, IEnumerable<Guid> links);
    Task<ServiceResponse<bool>> EditNode(string nodesetName, NodeDto nodeDto);
    Task<ServiceResponse<bool>> DeleteNode(string nodesetName, Guid guid);
    Task<ServiceResponse<int>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids);
}