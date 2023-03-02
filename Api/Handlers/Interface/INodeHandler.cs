using Dto;
using Dto.Models;

namespace Api.Handlers.Interface;

public interface INodeHandler
{
    Task<ServiceResponse<NodeDto>> GetNode(string name, string guid);
    Task<ServiceResponse<IEnumerable<NodeDto>>> GetAllNodes(string name);
    Task<ServiceResponse<IEnumerable<NodeDto>>> GetNodes(string name, IEnumerable<string> guids);
    Task<ServiceResponse<IEnumerable<NodeDto>>> CreateNodes(string nodesetName, IEnumerable<NodeDto> nodeDtos);
    Task<ServiceResponse<bool>> EditNodePosition(string nodesetName, string guid, IEnumerable<float> position);
    Task<ServiceResponse<bool>> EditNodeLinks(string nodesetName, string guid, IEnumerable<string> links);
    Task<ServiceResponse<bool>> EditNode(string nodesetName, NodeDto nodeDto);
    Task<ServiceResponse<bool>> DeleteNode(string nodesetName, string guid);
    Task<ServiceResponse<int>> DeleteNodes(string nodesetName, IEnumerable<string> guids);
}