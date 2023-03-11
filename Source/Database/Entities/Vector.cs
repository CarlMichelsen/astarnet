namespace Astar.Database.Entities;

/// <summary>
/// Database entity that represents a Vector position.
/// Can be found in a nodeset node.
/// </summary>
public class Vector
{
    /// <summary>
    /// Gets or sets the X component in the vector.
    /// </summary>
    /// <value>Float X vector component.</value>
    required public float X { get; set; }

    /// <summary>
    /// Gets or sets the Y component in the vector.
    /// </summary>
    /// <value>Float Y vector component.</value>
    required public float Y { get; set; }

    /// <summary>
    /// Gets or sets the Z component in the vector.
    /// </summary>
    /// <value>Float Z vector component.</value>
    required public float Z { get; set; }
}