using Api.Handlers.Interface;
using Dto;
using Dto.Models;

namespace Api.Handlers;

public class PathHandler : IPathHandler
{
    public Task<ServiceResponse<IEnumerable<NodeDto>>> CalculatePath(string nodeset, PathRequestDto pathRequest)
    {
        throw new NotImplementedException();
    }
}