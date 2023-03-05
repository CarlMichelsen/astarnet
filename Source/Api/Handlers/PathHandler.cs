using Api.Handlers.Interface;
using BusinessLogic.Calculators.Heuristics;
using BusinessLogic.Calculators.Interface;
using Database.Repositories.Interface;
using BusinessLogic.Extensions;
using Dto;
using Dto.Models;
using BusinessLogic.Mappers;

namespace Api.Handlers;

public class PathHandler : IPathHandler
{
    private readonly IPathCalculator pathCalculator;
    private readonly INodesetRepository nodesetRepository;

    public PathHandler(IPathCalculator pathCalculator, INodesetRepository nodesetRepository)
    {
        this.pathCalculator = pathCalculator;
        this.nodesetRepository = nodesetRepository;
    }

    public async Task<ServiceResponse<IEnumerable<NodeDto>>> CalculatePath(string nodeset, PathRequestDto pathRequest)
    {

        var nodesetObj = await nodesetRepository.GetNodeset(nodeset);
        if (nodesetObj is null) throw new NullReferenceException("Could not find nodeset"); // TODO: find better exception for this
        var linkedList = await pathCalculator.Calculate(
            nodesetObj,
            pathRequest.StartNodeGuid,
            pathRequest.EndNodeGuid,
            BasicHeuristics.BasicDistanceToGoal,
            BasicHeuristics.BasicDistance,
            CancellationToken.None
        );

        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = linkedList.Select(guid => NodeMapper.NodeToDto(nodesetObj.GetNode(guid)))
        };
    }
}