using Database.Entities;

namespace BusinessLogic.Calculators.Interface;

public interface IPathCalculator
{
    delegate float HeuristicDistanceToGoal(Node node, Node goal);
    delegate float HeuristicDistance(Node currentNode, Node neighborNode);
    public Task<LinkedList<Guid>> Calculate(Nodeset nodes, Guid start, Guid goal, HeuristicDistanceToGoal h, HeuristicDistance d, CancellationToken cancellationToken);
}