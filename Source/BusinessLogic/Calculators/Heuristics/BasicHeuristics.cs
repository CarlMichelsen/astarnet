using Astar.BusinessLogic.Calculators.Interface;
using Astar.BusinessLogic.Extensions;
using Astar.Database.Entities;

namespace Astar.BusinessLogic.Calculators.Heuristics;

/// <summary>
/// Distance estimation methods for use in PathCalculator.
/// </summary>
public static class BasicHeuristics
{
    /// <summary>
    /// Gets distance from a node to goal.
    /// </summary>
    /// <returns>Distance as a float.</returns>
    public static IPathCalculator.HeuristicDistance DistanceEstimation => (Node node, Node otherNode) =>
    {
        return node.Position.DistanceTo(otherNode.Position);
    };
}