namespace Astar.Dto.Models;

/// <summary>
/// A request for a path calculation DTO.
/// </summary>
public class PathRequestDto
{
    /// <summary>
    /// Gets or sets the guid identifier for the *start* node in the path calculation request.
    /// </summary>
    /// <value>Guid node identifier.</value>
    public Guid StartNodeGuid { get; set; }

    /// <summary>
    /// Gets or sets the guid identifier for the *end* node in the path calculation request.
    /// </summary>
    /// <value>Guid node identifier.</value>
    public Guid EndNodeGuid { get; set; }
}