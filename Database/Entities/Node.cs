namespace Database.Entities;

public class Node
{
    public required Guid Id { get; set; }
    public required List<Guid> Links { get; set; }
    public required Vector Position { get; set; }
}