namespace Astar.Database.Entities;

/// <summary>
/// Database entity that represents a Node.
/// </summary>
public class Node
{
    /// <summary>
    /// Gets or sets Guid Id for the node.
    /// </summary>
    /// <value>Guid identifier.</value>
    required public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets Links for the node.
    /// </summary>
    /// <value>List of Guid node identifiers for links.</value>
    required public List<Guid> Links { get; set; }

    /// <summary>
    /// Gets or sets Position for the node.
    /// </summary>
    /// <value>A Vector that represents the position of the node.</value>
    required public Vector Position { get; set; }
}