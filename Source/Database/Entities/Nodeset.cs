namespace Astar.Database.Entities;

/// <summary>
/// Database entity that represents a Nodeset.
/// </summary>
public class Nodeset
{
    /// <summary>
    /// Gets or sets name of the nodeset.
    /// </summary>
    /// <value>Name as a string.</value>
    required public string Name { get; set; }

    /// <summary>
    /// Gets or sets nodes in the nodeset.
    /// </summary>
    /// <value>Node dictionary with guid identifier.</value>
    required public Dictionary<Guid, Node> Nodes { get; set; }
}