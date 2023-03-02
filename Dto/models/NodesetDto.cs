namespace Dto.Models;

public struct NodesetDto
{
    public required string Name { get; set; }
    public required Dictionary<Guid, NodeDto> Nodes { get; set; }
}