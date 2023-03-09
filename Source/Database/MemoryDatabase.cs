using Astar.Database.Entities;

namespace Astar.Database;

public class MemoryDatabase
{
    public Dictionary<string, Nodeset> Data { get; } = new Dictionary<string, Nodeset>();
}