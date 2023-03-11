using Astar.Database.Entities;
using Astar.Database.Repositories.Interface;

namespace Astar.Database.Repositories;

/// <summary>
/// Implementation of INodeRepository.
/// </summary>
public class MemoryNodeRepository : INodeRepository
{
    private readonly MemoryDatabase database;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryNodeRepository"/> class.
    /// </summary>
    /// <param name="database">In-memory database for testing.</param>
    public MemoryNodeRepository(MemoryDatabase database)
    {
        this.database = database;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Node>> CreateNodes(string nodesetName, IEnumerable<Node> nodes)
    {
        await Task.Delay(10); // Pretend to do work.
        lock (this.database.Data)
        {
            var list = new List<Node>();
            var foundNodeset = this.database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
            if (!foundNodeset)
            {
                return list;
            }

            var addedNodes = new List<Node>();
            var addedMap = new Dictionary<Guid, Guid>();
            foreach (var node in nodes)
            {
                var added = nodeset!.Nodes.TryAdd(node.Id, node);
                if (!added)
                {
                    continue;
                }

                var links = node.Links.ToDictionary(x => node.Id, x => x);
                addedMap = addedMap.Union(links).ToDictionary(x => x.Key, x => x.Value);
                addedNodes.Add(node);
            }

            RegisterAddedLinks(nodeset!, addedMap);

            return addedNodes;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Node>> EditNodes(string nodesetName, IEnumerable<Node> nodes)
    {
        await Task.Delay(10); // Pretend to do work.
        lock (this.database.Data)
        {
            var list = new List<Node>();
            var foundNodeset = this.database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
            if (!foundNodeset)
            {
                return list;
            }

            var editedNodes = new List<Node>();
            var addedMap = new Dictionary<Guid, Guid>();
            var removedMap = new Dictionary<Guid, Guid>();
            foreach (var node in nodes)
            {
                var found = nodeset!.Nodes.TryGetValue(node.Id, out Node? exsistingNode);
                if (!found)
                {
                    continue;
                }

                var added = node.Links
                    .Where(l => !exsistingNode!.Links.Contains(l))
                    .ToDictionary(x => node.Id, x => x);
                addedMap = addedMap.Union(added)
                    .ToDictionary(x => x.Key, x => x.Value);

                var removed = exsistingNode!.Links
                    .Where(l => !node.Links.Contains(l))
                    .ToDictionary(x => node.Id, x => x);
                removedMap = removedMap.Union(removed)
                    .ToDictionary(x => x.Key, x => x.Value);

                exsistingNode.Links = node.Links;
                exsistingNode.Position = node.Position;
                editedNodes.Add(node);
            }

            RegisterAddedLinks(nodeset!, addedMap);
            RegisterRemovedLinks(nodeset!, removedMap);

            return editedNodes;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Node>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        await Task.Delay(10); // Pretend to do work.
        lock (this.database.Data)
        {
            var list = new List<Node>();
            var foundNodeset = this.database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
            if (!foundNodeset)
            {
                return list;
            }

            var deletedNodes = new List<Node>();
            var removedMap = new Dictionary<Guid, Guid>();
            foreach (var guid in guids)
            {
                var found = nodeset!.Nodes.TryGetValue(guid, out Node? exsistingNode);
                if (!found)
                {
                    continue;
                }

                var successfulremove = nodeset!.Nodes.Remove(guid);
                if (!successfulremove)
                {
                    continue;
                }

                var removed = exsistingNode!.Links
                    .ToDictionary(x => exsistingNode.Id, x => x);
                removedMap = removedMap.Union(removed)
                    .ToDictionary(x => x.Key, x => x.Value);

                deletedNodes.Add(exsistingNode);
            }

            RegisterRemovedLinks(nodeset!, removedMap);

            return deletedNodes;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Node>> GetNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        await Task.Delay(10); // Pretend to do work.
        lock (this.database.Data)
        {
            var list = new List<Node>();
            var foundNodeset = this.database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
            if (!foundNodeset)
            {
                return list;
            }

            var nodes = new List<Node>();
            foreach (var guid in guids)
            {
                var found = nodeset!.Nodes.TryGetValue(guid, out Node? exsistingNode);
                if (!found)
                {
                    continue;
                }

                nodes.Add(exsistingNode!);
            }

            return nodes;
        }
    }

    private static void RegisterAddedLinks(Nodeset nodeset, Dictionary<Guid, Guid> fromToMap)
    {
        foreach (var ft in fromToMap)
        {
            var found = nodeset.Nodes.TryGetValue(ft.Value, out Node? node);
            if (!found)
            {
                continue;
            }

            node!.Links.Add(ft.Key);
        }
    }

    private static void RegisterRemovedLinks(Nodeset nodeset, Dictionary<Guid, Guid> fromToMap)
    {
        foreach (var ft in fromToMap)
        {
            var found = nodeset.Nodes.TryGetValue(ft.Value, out Node? node);
            if (!found)
            {
                continue;
            }

            node!.Links.Remove(ft.Key);
        }
    }
}