using BusinessLogic.Calculators.Interface;
using Database.Entities;

namespace BusinessLogic.Calculators.Heuristics;

public static class BasicHeuristics
{
    public static IPathCalculator.HeuristicDistanceToGoal BasicDistanceToGoal => (Node node, Node goal) =>
    {
        return node.Position.DistanceTo(goal.Position);
    };

    public static IPathCalculator.HeuristicDistance BasicDistance => (Node currentNode, Node neighborNode) =>
    {
        return BasicDistanceToGoal(currentNode, neighborNode);
    };
}