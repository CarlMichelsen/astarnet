using Astar.Database.Entities;

namespace Astar.Database.Repositories.Interface;

public interface INodeRepository
{
    Task<IEnumerable<Node>> CreateNodes(string nodesetName, IEnumerable<Node> nodes);
    Task<bool> EditNodePosition(string nodesetName, Guid guid, Vector position);
    Task<bool> EditNodeLinks(string nodesetName, Guid guid, IEnumerable<Guid> links);
    Task<bool> EditNode(string nodesetName, Guid guid, Vector position, IEnumerable<Guid> links);
    Task<Node?> GetNode(string nodesetName, Guid guid);
    Task<IEnumerable<Node>> GetNodes(string nodesetName, IEnumerable<Guid> guids);
    Task<IEnumerable<Node>> GetAllNodes(string nodesetName);
    Task<bool> DeleteNode(string nodesetName, Guid guid);
    Task<int> DeleteNodes(string nodesetName, IEnumerable<Guid> guids);
}