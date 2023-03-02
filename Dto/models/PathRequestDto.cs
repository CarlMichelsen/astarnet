namespace Dto.Models;

public struct PathRequestDto
{
    public Guid StartNodeGuid { get; set; }
    public Guid EndNodeGuid { get; set; }
}