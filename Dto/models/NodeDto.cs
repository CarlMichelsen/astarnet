namespace Dto.Models;

public struct NodeDto
{
    public string Id { get; set; }
    public required IEnumerable<float> Position { get; set; }
    public required IEnumerable<string> Links { get; set; }
}
