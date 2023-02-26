using Database.Entities;

namespace Database.Repositories.Interface;

public interface INodesetRepository
{
    Task<bool> CreateNodeset(string name);
    Task<bool> EditNodeset(string name, string newName);
    Task<Nodeset?> GetNodeset(string name);
    Task<IEnumerable<Nodeset>> GetAllNodesets();
    Task<bool> DeleteNodeset(string name);
}