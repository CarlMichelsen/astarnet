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
            Nodes = nodeset.Nodes.ToDictionary(x => x.Key.ToString(), x => NodeToDto(x.Value))
        };
    }

    public static NodeDto NodeToDto(Node node)
    {
        return new NodeDto
        {
            Id = string.IsNullOrWhiteSpace(node.Id.ToString()) ? string.Empty : node.Id.ToString(),
            Position = VectorToDto(node.Position),
            Links = node.Links.Select(x => x.ToString())
        };
    }

    public static List<float> VectorToDto(Vector vec)
    {
        return new List<float> {
                vec.X,
                vec.Y,
                vec.Z
            };
    }

    public static Nodeset DtoToNodeset(NodesetDto nodeset)
    {
        return new Nodeset
        {
            Name = nodeset.Name,
            Nodes = nodeset.Nodes.ToDictionary(x => Guid.Parse(x.Key), x => DtoToNode(x.Value))
        };
    }

    public static Node DtoToNode(NodeDto node)
    {
        return new Node
        {
            Id = string.IsNullOrWhiteSpace(node.Id) ? Guid.NewGuid() : Guid.Parse(node.Id),
            Position = DtoToVector(node.Position.ToList()),
            Links = node.Links.Select(Guid.Parse).ToList()
        };
    }

    public static Vector DtoToVector(List<float> vec)
    {
        return new Vector
        {
            X = vec[0],
            Y = vec[1],
            Z = vec[2]
        };
    }
}