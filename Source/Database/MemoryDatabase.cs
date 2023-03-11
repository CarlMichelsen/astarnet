using Astar.Database.Entities;

namespace Astar.Database;

/// <summary>
/// Test in-memory database.
/// </summary>
public class MemoryDatabase
{
    /// <summary>
    /// Gets the data inside the in-memory database.
    /// </summary>
    /// <returns>A dictionary with nodesets.</returns>
    public Dictionary<string, Nodeset> Data { get; } = new Dictionary<string, Nodeset>();
}