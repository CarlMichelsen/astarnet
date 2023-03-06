using Database.Entities;

namespace BusinessLogic.Calculators.Interface;

/// <summary>
/// Interface for path calculators.
/// </summary>
public interface IPathCalculator
{
    /// <summary>
    /// HeuristicDistance (to neighbor) delegate function.
    /// </summary>
    /// <param name="currentNode">Current node.</param>
    /// <param name="otherNode">Neighboring node.</param>
    /// <returns>Distance as float.</returns>
    public delegate float HeuristicDistance(Node currentNode, Node otherNode);

    /// <summary>
    /// Calculate shortest path between two nodes in a nodeset.
    /// </summary>
    /// <param name="nodes">Nodeset to do calculations in.</param>
    /// <param name="start">Start node Guid.</param>
    /// <param name="goal">End node Guid.</param>
    /// <param name="h">HeuristicDistanceToGoal delegate function.</param>
    /// <param name="cancellationToken">Allows cancellation of calculation.</param>
    /// <returns>LinkedList of Guid node identifiers. That defines the shortest path from start to goal.</returns>
    public Task<LinkedList<Guid>> Calculate(Nodeset nodes, Guid start, Guid goal, HeuristicDistance h, CancellationToken cancellationToken);
}