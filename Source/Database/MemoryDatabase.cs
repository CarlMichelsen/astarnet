using Database.Entities;

namespace Database;

public class MemoryDatabase
{
    public Dictionary<string, Nodeset> Data { get; } = new Dictionary<string, Nodeset>();
}