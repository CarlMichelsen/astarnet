namespace Dto.Models;

public struct NodesetDto
{
    public required string Name { get; set; }
    public required Dictionary<string, NodeDto> Nodes { get; set; }
}