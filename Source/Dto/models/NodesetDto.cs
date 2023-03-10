namespace Astar.Dto.Models;

/// <summary>
/// A DTO that represents a whole nodeset.
/// </summary>
public class NodesetDto
{
    /// <summary>
    /// Gets or sets the name of a nodesetDto.
    /// </summary>
    /// <value>Name of the nodeset.</value>
    required public string Name { get; set; }

    /// <summary>
    /// Gets or sets all the nodes in the nodeset.
    /// </summary>
    /// <value>A dictionary that contains all the nodes in the nodeset using the guid identifier.</value>
    required public Dictionary<Guid, NodeDto> Nodes { get; set; }
}