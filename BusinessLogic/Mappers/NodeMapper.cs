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
}