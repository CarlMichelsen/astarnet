using Database.Entities;
using Dto.Models;

namespace Database.Repositories.Interface;

public interface INodeRepository
{
    IEnumerable<Node> CreateNodes(Nodeset nodeset, IEnumerable<NodeDto> nodes);
    bool EditNodePosition(Nodeset nodeset, Guid guid, Vector position);
    bool EditNodeLinks(Nodeset nodeset, Guid guid, IEnumerable<Guid> links);
    bool EditNode(Nodeset nodeset, Guid guid, Vector position, IEnumerable<Guid> links);
    Node? GetNode(Nodeset nodeset, Guid guid);
    IEnumerable<Node> GetNodes(Nodeset nodeset, IEnumerable<Guid> guids);
    bool DeleteNode(Nodeset nodeset, Guid guid);
    int DeleteNodes(Nodeset nodeset, IEnumerable<Guid> guids);
}