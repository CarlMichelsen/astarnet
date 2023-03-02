using Database.Entities;
using Database.Repositories.Interface;

namespace Database.Repositories;

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
        if (foundNode) node!.Links = links.ToList();
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

    public Task<IEnumerable<Node>> GetAllNodes(string nodesetName)
    {
        throw new NotImplementedException();
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
}