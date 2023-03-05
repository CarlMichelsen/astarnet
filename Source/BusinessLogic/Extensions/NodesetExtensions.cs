using Database.Entities;

namespace BusinessLogic.Extensions;

public static class NodesetExtensions
{
    public static Node GetNode(this Nodeset nodeset, Guid nodeGuid)
    {
        var found = nodeset.Nodes.TryGetValue(nodeGuid, out Node? node);
        if (found) return node!;
        throw new NullReferenceException("Could not find node in nodeset");
    }
}