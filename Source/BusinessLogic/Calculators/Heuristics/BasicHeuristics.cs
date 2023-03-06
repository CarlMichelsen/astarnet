using BusinessLogic.Calculators.Interface;
using Database.Entities;

namespace BusinessLogic.Calculators.Heuristics;

/// <summary>
/// Distance estimation methods for use in PathCalculator.
/// </summary>
public static class BasicHeuristics
{
    /// <summary>
    /// Gets distance from a node to goal.
    /// </summary>
    /// <returns>Distance as a float.</returns>
    public static IPathCalculator.HeuristicDistanceToGoal BasicDistanceToGoal => (Node node, Node goal) =>
    {
        return node.Position.DistanceTo(goal.Position);
    };

    /// <summary>
    /// Gets distance from a node to its' neighbor.
    /// </summary>
    /// <returns>Distance as a float.</returns>
    public static IPathCalculator.HeuristicDistance BasicDistance => (Node currentNode, Node neighborNode) =>
    {
        return BasicDistanceToGoal(currentNode, neighborNode);
    };
}