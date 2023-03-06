using Database.Entities;

namespace BusinessLogic.Extensions;

/// <summary>
/// Extensionmethods for Nodeset.
/// </summary>
public static class NodesetExtensions
{
    /// <summary>
    /// Get a node from a nodeset and assume it's there.
    /// </summary>
    /// <param name="nodeset">Nodeset.</param>
    /// <param name="nodeGuid">Guid of the node you're trying to get.</param>
    /// <returns>The node in the nodeset with nodeGuid.</returns>
    public static Node GetNode(this Nodeset nodeset, Guid nodeGuid)
    {
        var found = nodeset.Nodes.TryGetValue(nodeGuid, out Node? node);
        if (found)
        {
            return node!;
        }

        throw new NullReferenceException("Could not find node in nodeset");
    }
}