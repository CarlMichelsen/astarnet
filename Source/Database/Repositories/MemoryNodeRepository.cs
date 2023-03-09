using Astar.Database.Entities;
using Astar.Database.Repositories.Interface;

namespace Astar.Database.Repositories;

public class MemoryNodeRepository : INodeRepository
{
    private readonly MemoryDatabase database;

    public MemoryNodeRepository(MemoryDatabase database)
    {
        this.database = database;
    }

    public async Task<IEnumerable<Node>> CreateNodes(string nodesetName, IEnumerable<Node> nodes)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);

        var added = new List<Node>();
        if (!found) return added;

        foreach (var node in nodes)
        {
            var newNode = new Node
            {
                Id = node.Id == Guid.Empty ? Guid.NewGuid() : node.Id,
                Position = node.Position,
                Links = node.Links
            };
            var success = await ValidateLinks(nodesetName, node!.Links);
            if (!success) return added;

            await HandleLinkDiff(nodesetName, newNode.Id, newNode.Links, new List<Guid>());

            var successfulAdd = nodeset!.Nodes.TryAdd(newNode.Id, newNode);
            if (successfulAdd) added.Add(newNode);
        }
        return added;
    }

    public async Task<bool> DeleteNode(string nodesetName, Guid guid)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!found) return false;

        await HandleLinkDiff(
            nodesetName,
            guid,
            new List<Guid>(),
            nodeset!.Nodes.GetValueOrDefault(guid)?.Links ?? new List<Guid>());
        return nodeset!.Nodes.Remove(guid);
    }

    public async Task<int> DeleteNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!found) return 0;

        var deleted = 0;
        foreach (var guid in guids)
        {
            await HandleLinkDiff(
                nodesetName,
                guid,
                new List<Guid>(),
                nodeset!.Nodes.GetValueOrDefault(guid)?.Links ?? new List<Guid>());
            if (nodeset!.Nodes.Remove(guid)) deleted++;
        }
        return deleted;
    }

    public async Task<bool> EditNode(string nodesetName, Guid guid, Vector position, IEnumerable<Guid> links)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!found) return false;

        var foundNode = nodeset!.Nodes.TryGetValue(guid, out Node? node);
        if (foundNode)
        {
            var success = await ValidateLinks(nodesetName, node!.Links);
            if (!success) return false;

            var addedLinks = AddedLinks(node!.Links, links);
            var removedLinks = RemovedLinks(node!.Links, links);
            await HandleLinkDiff(nodesetName, guid, addedLinks, removedLinks);

            node!.Position = position;
            node!.Links = links.ToList();
        }
        return foundNode;
    }

    public async Task<bool> EditNodeLinks(string nodesetName, Guid guid, IEnumerable<Guid> links)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!found) return false;

        var foundNode = nodeset!.Nodes.TryGetValue(guid, out Node? node);
        if (foundNode)
        {
            var success = await ValidateLinks(nodesetName, node!.Links);
            if (!success) return false;

            var addedLinks = AddedLinks(node!.Links, links);
            var removedLinks = RemovedLinks(node!.Links, links);
            await HandleLinkDiff(nodesetName, guid, addedLinks, removedLinks);

            node!.Links = links.ToList();
        }
        return foundNode;
    }

    public async Task<bool> EditNodePosition(string nodesetName, Guid guid, Vector position)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!found) return false;

        var foundNode = nodeset!.Nodes.TryGetValue(guid, out Node? node);
        if (foundNode) node!.Position = position;
        return foundNode;
    }

    public async Task<IEnumerable<Node>> GetAllNodes(string nodesetName)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!found) return new List<Node>();
        return nodeset!.Nodes.Values.ToList();
    }

    public async Task<Node?> GetNode(string nodesetName, Guid guid)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!found) return default;

        return nodeset!.Nodes.GetValueOrDefault(guid);
    }

    public async Task<IEnumerable<Node>> GetNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        var nodes = new List<Node>();
        if (!found) return nodes;

        foreach (var guid in guids)
        {
            var foundNode = nodeset!.Nodes.TryGetValue(guid, out Node? node);
            if (foundNode) nodes.Add(node!);
        }
        return nodes;
    }

    private static IEnumerable<Guid> AddedLinks(IEnumerable<Guid> oldLinks, IEnumerable<Guid> newLinks)
    {
        var added = new List<Guid>();

        foreach (var link in newLinks)
        {
            if (!oldLinks.Contains(link))
            {
                added.Add(link);
            }
        }

        return added;
    }

    private static IEnumerable<Guid> RemovedLinks(IEnumerable<Guid> oldLinks, IEnumerable<Guid> newLinks)
    {
        var removed = new List<Guid>();

        foreach (var link in oldLinks)
        {
            if (!newLinks.Contains(link))
            {
                removed.Add(link);
            }
        }

        return removed;
    }

    private async Task<bool> ValidateLinks(string nodesetName, IEnumerable<Guid> links)
    {
        await Task.Delay(10); // Pretend to do work :)
        var foundNodeset = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!foundNodeset) return false;

        foreach (var link in links)
        {
            var found = nodeset!.Nodes.TryGetValue(link, out Node? _);
            if (!found) return false;
        }

        return true;
    }

    private async Task HandleLinkDiff(string nodesetName, Guid current, IEnumerable<Guid> addedLinks, IEnumerable<Guid> removedLinks)
    {
        await Task.Delay(10); // Pretend to do work :)
        var foundNodeset = database.Data.TryGetValue(nodesetName, out Nodeset? nodeset);
        if (!foundNodeset) return;

        foreach (var link in addedLinks)
        {
            var found = nodeset!.Nodes.TryGetValue(link, out Node? node);
            if (!found)
            {
                continue;
            }

            node!.Links.Add(current);
        }

        foreach (var link in removedLinks)
        {
            var found = nodeset!.Nodes.TryGetValue(link, out Node? node);
            if (!found)
            {
                continue;
            }

            node!.Links.Remove(current);
        }
    }
}