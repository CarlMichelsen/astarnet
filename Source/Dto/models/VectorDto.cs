namespace Astar.Dto.Models;

/// <summary>
/// A DTO that represents a vector position. Usually for a node in a nodeset.
/// </summary>
public struct VectorDto
{
    /// <summary>
    /// Gets or sets the X component of the VectorDto.
    /// </summary>
    /// <value>Float number for the X component in the vector.</value>
    public float X { get; set; }

    /// <summary>
    /// Gets or sets the Y component of the VectorDto.
    /// </summary>
    /// <value>Float number for the Y component in the vector.</value>
    public float Y { get; set; }

    /// <summary>
    /// Gets or sets the Z component of the VectorDto.
    /// </summary>
    /// <value>Float number for the Z component in the vector.</value>
    public float Z { get; set; }
}