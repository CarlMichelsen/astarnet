using Database.Entities;
using Dto.Models;

namespace BusinessLogic.Mappers;

public static class NodeMapper
{

    public static NodesetDto NodesetToDto(Nodeset nodeset)
    {
        return new NodesetDto
        {
            Name = nodeset.Name,
            Nodes = nodeset.Nodes.ToDictionary(x => x.Key, x => NodeToDto(x.Value))
        };
    }

    public static NodeDto NodeToDto(Node node)
    {
        return new NodeDto
        {
            Id = node.Id == Guid.Empty ? Guid.Empty : node.Id,
            Position = VectorToDto(node.Position),
            Links = node.Links
        };
    }

    public static VectorDto VectorToDto(Vector vec)
    {
        return new VectorDto
        {
            X = vec.X,
            Y = vec.Y,
            Z = vec.Z
        };
    }

    public static Nodeset DtoToNodeset(NodesetDto nodeset)
    {
        return new Nodeset
        {
            Name = nodeset.Name,
            Nodes = nodeset.Nodes.ToDictionary(x => x.Key, x => DtoToNode(x.Value))
        };
    }

    public static Node DtoToNode(NodeDto node)
    {
        return new Node
        {
            Id = node.Id == Guid.Empty ? Guid.NewGuid() : node.Id,
            Position = DtoToVector(node.Position),
            Links = node.Links.ToList()
        };
    }

    public static Vector DtoToVector(VectorDto vec)
    {
        return new Vector
        {
            X = vec.X,
            Y = vec.Y,
            Z = vec.Z
        };
    }
}