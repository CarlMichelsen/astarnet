using Astar.Api.Handlers.Interface;
using Astar.BusinessLogic.Calculators.Heuristics;
using Astar.BusinessLogic.Calculators.Interface;
using Astar.BusinessLogic.Extensions;
using Astar.BusinessLogic.Mappers;
using Astar.Database.Repositories.Interface;
using Astar.Dto;
using Astar.Dto.Models;

namespace Astar.Api.Handlers;

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

    /// <inheritdoc />
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