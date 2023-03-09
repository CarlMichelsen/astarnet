namespace Astar.Dto.Models;

public struct NodeDto
{
    public Guid Id { get; set; }
    public required VectorDto Position { get; set; }
    public required IEnumerable<Guid> Links { get; set; }
}
