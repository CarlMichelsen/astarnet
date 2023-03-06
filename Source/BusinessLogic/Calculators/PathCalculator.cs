using BusinessLogic.Calculators.Interface;
using BusinessLogic.Exceptions;
using BusinessLogic.Extensions;
using Database.Entities;

namespace BusinessLogic.Calculators;

/// <summary>
/// Calculates the shortest possible path using A* with custom distance heuristic delegate functions.
/// </summary>
public class PathCalculator : IPathCalculator
{
    /// <summary>
    /// Async wrapper for CalculateSync.
    /// </summary>
    /// <param name="nodes">Nodeset to do calculations in.</param>
    /// <param name="start">Start node Guid.</param>
    /// <param name="goal">End node Guid.</param>
    /// <param name="h">HeuristicDistanceToGoal delegate function.</param>
    /// <param name="d">HeuristicDistance (to neighbor) delegate function.</param>
    /// <param name="cancellationToken">Allows cancellation of calculation.</param>
    /// <returns>LinkedList of Guid node identifiers. That defines the shortest path from start to goal.</returns>
    public async Task<LinkedList<Guid>> Calculate(
        Nodeset nodes,
        Guid start,
        Guid goal,
        IPathCalculator.HeuristicDistanceToGoal h,
        IPathCalculator.HeuristicDistance d,
        CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => CalculateSync(nodes, start, goal, h, d, cancellationToken));
    }

    /// <summary>
    /// Method that does actual A* calculation.
    /// </summary>
    /// <param name="nodes">Nodeset to do calculations in.</param>
    /// <param name="start">Start node Guid.</param>
    /// <param name="goal">End node Guid.</param>
    /// <param name="h">HeuristicDistanceToGoal delegate function.</param>
    /// <param name="d">HeuristicDistance (to neighbor) delegate function.</param>
    /// <param name="cancellationToken">Allows cancellation of calculation.</param>
    /// <returns>LinkedList of Guid node identifiers. That defines the shortest path from start to goal.</returns>
    private static LinkedList<Guid> CalculateSync(
        Nodeset nodes,
        Guid start,
        Guid goal,
        IPathCalculator.HeuristicDistanceToGoal h,
        IPathCalculator.HeuristicDistance d,
        CancellationToken cancellationToken)
    {
        var goalNode = nodes.GetNode(goal);
        var startNode = nodes.GetNode(start);

        Dictionary<Guid, bool> openSetMap = new()
        {
            { start, true },
        };

        Dictionary<Guid, Guid> cameFromMap = new();
        Dictionary<Guid, float> gScoreMap = new()
        {
            { start, 0 },
        };
        Dictionary<Guid, float> fScoreMap = new()
        {
            { start, h(startNode, goalNode) },
        };

        Guid current;
        while (openSetMap.Count > 0)
        {
            current = OpenSetNodeWithLowestFScore(openSetMap, fScoreMap);

            if (current == goal)
            {
                return ReconstructPath(cameFromMap, current);
            }

            // Respect CancellationToken after checking if goal has been reached
            cancellationToken.ThrowIfCancellationRequested();

            openSetMap.Remove(current);

            var currentNode = nodes.GetNode(current);
            foreach (var neighborNode in currentNode.Links.Select(nodes.GetNode))
            {
                var tentativeGScore = DefaultInfinityGet(gScoreMap, current) + d(currentNode, neighborNode);

                if (tentativeGScore < DefaultInfinityGet(gScoreMap, neighborNode.Id))
                {
                    cameFromMap.Add(neighborNode.Id, current);
                    gScoreMap.Add(neighborNode.Id, tentativeGScore);
                    fScoreMap.Add(neighborNode.Id, tentativeGScore + h(neighborNode, goalNode));
                    if (!openSetMap.ContainsKey(neighborNode.Id))
                    {
                        openSetMap.Add(neighborNode.Id, true);
                    }
                }
            }
        }

        throw new PathCalculationFailedException("Path is not possible");
    }

    private static LinkedList<Guid> ReconstructPath(Dictionary<Guid, Guid> cameFrom, Guid current)
    {
        var path = new LinkedList<Guid>();
        path.AddFirst(current);
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom.GetValueOrDefault(current);
            if (current == Guid.Empty)
            {
                throw new PathCalculationFailedException("Failure during ReconstructPath phase");
            }

            path.AddFirst(current);
        }

        return path;
    }

    private static Guid OpenSetNodeWithLowestFScore(Dictionary<Guid, bool> openSetMap, Dictionary<Guid, float> fScoreMap)
    {
        var node = Guid.Empty;
        var lowest = float.MaxValue;
        foreach (var kv in openSetMap)
        {
            var fScore = DefaultInfinityGet(fScoreMap, kv.Key);
            if (fScore < lowest)
            {
                node = kv.Key;
            }
        }

        if (node == Guid.Empty)
        {
            throw new PathCalculationFailedException("Did not find any nodes with lowest fScore");
        }

        return node;
    }

    private static float DefaultInfinityGet(Dictionary<Guid, float> thisFScoreMap, Guid guid)
    {
        var found = thisFScoreMap.TryGetValue(guid, out float fScore);
        if (found)
        {
            return fScore;
        }

        return float.MaxValue;
    }
}