namespace Astar.Database.Entities;

public class Nodeset
{
    public required string Name { get; set; }
    public required Dictionary<Guid, Node> Nodes { get; set; }
}