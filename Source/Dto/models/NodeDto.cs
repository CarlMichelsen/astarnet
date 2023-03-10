namespace Astar.Dto.Models;

/// <summary>
/// A dto that represents a node from a nodeset.
/// </summary>
public class NodeDto
{
    /// <summary>
    /// Gets or sets identifier for the node. This can be left unset when creating new nodes.
    /// </summary>
    /// <value>Guid identifier.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the position of the node.
    /// </summary>
    /// <value>Representation of a vector in a DTO.</value>
    required public VectorDto Position { get; set; }

    /// <summary>
    /// Gets or sets the links of the node.
    /// This can be left unset when used to create a node without links.
    /// </summary>
    /// <value>List of guid identifiers for linked nodes. Links are always two-way for now.</value>
    public IEnumerable<Guid>? Links { get; set; }
}
