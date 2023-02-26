using Database.Entities;
using Database.Repositories.Interface;
using Dto.Models;

namespace Database.Repositories;

public class MemoryNodeRepository : INodeRepository
{
    public IEnumerable<Node> CreateNodes(Nodeset nodeset, IEnumerable<NodeDto> nodes)
    {
        var added = new List<Node>();
        foreach (var nodeDto in nodes)
        {
            var positionList = nodeDto.Position.ToList();
            var newNode = new Node
            {
                Id = string.IsNullOrWhiteSpace(nodeDto.Id) ? Guid.NewGuid() : Guid.Parse(nodeDto.Id),
                Position = new Vector { X = positionList[0], Y = positionList[1], Z = positionList[2] },
                Links = nodeDto.Links.Select(Guid.Parse).ToList()
            };
            var successfulAdd = nodeset.Nodes.TryAdd(newNode.Id, newNode);
            if (successfulAdd) added.Add(newNode);
        }
        return added;
    }

    public bool DeleteNode(Nodeset nodeset, Guid guid)
    {
        return nodeset.Nodes.Remove(guid);
    }

    public int DeleteNodes(Nodeset nodeset, IEnumerable<Guid> guids)
    {
        var deleted = 0;
        foreach (var guid in guids)
        {
            if (nodeset.Nodes.Remove(guid)) deleted++;
        }
        return deleted;
    }

    public bool EditNode(Nodeset nodeset, Guid guid, Vector position, IEnumerable<Guid> links)
    {
        var found = nodeset.Nodes.TryGetValue(guid, out Node? node);
        if (found)
        {
            node!.Position = position;
            node!.Links = links.ToList();
        }
        return found;
    }

    public bool EditNodeLinks(Nodeset nodeset, Guid guid, IEnumerable<Guid> links)
    {
        var found = nodeset.Nodes.TryGetValue(guid, out Node? node);
        if (found) node!.Links = links.ToList();
        return found;
    }

    public bool EditNodePosition(Nodeset nodeset, Guid guid, Vector position)
    {
        var found = nodeset.Nodes.TryGetValue(guid, out Node? node);
        if (found) node!.Position = position;
        return found;
    }

    public Node? GetNode(Nodeset nodeset, Guid guid)
    {
        return nodeset.Nodes.GetValueOrDefault(guid);
    }

    public IEnumerable<Node> GetNodes(Nodeset nodeset, IEnumerable<Guid> guids)
    {
        var nodes = new List<Node>();
        foreach (var guid in guids)
        {
            var found = nodeset.Nodes.TryGetValue(guid, out Node? node);
            if (found) nodes.Add(node!);
        }
        return nodes;
    }
}