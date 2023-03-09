using Astar.Database.Entities;
using Astar.Dto.Models;

namespace Astar.BusinessLogic.Mappers;

/// <summary>
/// Mapper methods for nodes, nodesets and their Dto counterparts.
/// </summary>
public static class NodeMapper
{
    /// <summary>
    /// Create a NodesetDto from a Nodeset entity.
    /// </summary>
    /// <param name="nodeset">Nodeset to convert.</param>
    /// <returns>NodesetDto from nodeset.</returns>
    public static NodesetDto NodesetToDto(Nodeset nodeset)
    {
        return new NodesetDto
        {
            Name = nodeset.Name,
            Nodes = nodeset.Nodes.ToDictionary(x => x.Key, x => NodeToDto(x.Value)),
        };
    }

    /// <summary>
    /// Create a NodeDto from a Node entity.
    /// </summary>
    /// <param name="node">Node to convert.</param>
    /// <returns>NodeDto from Node.</returns>
    public static NodeDto NodeToDto(Node node)
    {
        return new NodeDto
        {
            Id = node.Id == Guid.Empty ? Guid.Empty : node.Id,
            Position = VectorToDto(node.Position),
            Links = node.Links,
        };
    }

    /// <summary>
    /// Create a VectorDto from a Vector entity.
    /// </summary>
    /// <param name="vec">Vector to convert.</param>
    /// <returns>VectorDto from Vector.</returns>
    public static VectorDto VectorToDto(Vector vec)
    {
        return new VectorDto
        {
            X = vec.X,
            Y = vec.Y,
            Z = vec.Z,
        };
    }

    /// <summary>
    /// Create a Nodeset entity from a NodesetDto.
    /// </summary>
    /// <param name="nodeset">NodesetDto to convert.</param>
    /// <returns>Nodeset from NodesetDto.</returns>
    public static Nodeset DtoToNodeset(NodesetDto nodeset)
    {
        return new Nodeset
        {
            Name = nodeset.Name,
            Nodes = nodeset.Nodes.ToDictionary(x => x.Key, x => DtoToNode(x.Value)),
        };
    }

    /// <summary>
    /// Create Node entity from NodeDto.
    /// </summary>
    /// <param name="node">NodeDto to convert.</param>
    /// <returns>Node from NodeDto.</returns>
    public static Node DtoToNode(NodeDto node)
    {
        return new Node
        {
            Id = node.Id == Guid.Empty ? Guid.NewGuid() : node.Id,
            Position = DtoToVector(node.Position),
            Links = node.Links.ToList(),
        };
    }

    /// <summary>
    /// Create a Vector entity from VectorDto.
    /// </summary>
    /// <param name="vec">VectorDto to convert.</param>
    /// <returns>Vector from VectorDto.</returns>
    public static Vector DtoToVector(VectorDto vec)
    {
        return new Vector
        {
            X = vec.X,
            Y = vec.Y,
            Z = vec.Z,
        };
    }
}