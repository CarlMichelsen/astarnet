using Dto;
using Dto.Models;

namespace Api.Handlers.Interface;

public interface IPathHandler
{
    public Task<ServiceResponse<IEnumerable<NodeDto>>> CalculatePath(string nodeset, PathRequestDto pathRequest);
}