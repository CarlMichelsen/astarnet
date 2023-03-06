using Api.Handlers.Interface;
using BusinessLogic.Calculators.Heuristics;
using BusinessLogic.Calculators.Interface;
using BusinessLogic.Extensions;
using BusinessLogic.Mappers;
using Database.Repositories.Interface;
using Dto;
using Dto.Models;

namespace Api.Handlers;

/// <summary>
/// A handler for doing path calculations.
/// This is primarily used in the PathController.
/// </summary>
public class PathHandler : IPathHandler
{
    private readonly IPathCalculator pathCalculator;
    private readonly INodesetRepository nodesetRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PathHandler"/> class.
    /// A constructor for PathHandler that takes dependencies as parameters through DI.
    /// </summary>
    /// <param name="pathCalculator">Calculates paths.</param>
    /// <param name="nodesetRepository">Reads and writes to nodeset data.</param>
    public PathHandler(IPathCalculator pathCalculator, INodesetRepository nodesetRepository)
    {
        this.pathCalculator = pathCalculator;
        this.nodesetRepository = nodesetRepository;
    }

    /// <summary>
    /// Actually calculate a path from a PathRequestDto.
    /// </summary>
    /// <param name="nodeset">Name of the nodeset you want to calculate a path in.</param>
    /// <param name="pathRequest">A Dto that contains information required to perform the path calculation.</param>
    /// <returns>A list of NodeDto that defines a path according to the pathRequest.</returns>
    public async Task<ServiceResponse<IEnumerable<NodeDto>>> CalculatePath(string nodeset, PathRequestDto pathRequest)
    {
        var nodesetObj = await this.nodesetRepository.GetNodeset(nodeset);
        if (nodesetObj is null)
        {
            throw new NullReferenceException("Could not find nodeset"); // TODO: find better exception for this
        }

        var linkedList = await this.pathCalculator.Calculate(
            nodesetObj,
            pathRequest.StartNodeGuid,
            pathRequest.EndNodeGuid,
            BasicHeuristics.DistanceEstimation,
            CancellationToken.None);

        return new ServiceResponse<IEnumerable<NodeDto>>
        {
            Data = linkedList.Select(guid => NodeMapper.NodeToDto(nodesetObj.GetNode(guid))),
        };
    }
}