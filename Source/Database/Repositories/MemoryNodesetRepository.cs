using Astar.Database.Entities;
using Astar.Database.Repositories.Interface;

namespace Astar.Database.Repositories;

public class MemoryNodesetRepository : INodesetRepository
{
    private readonly MemoryDatabase database;

    public MemoryNodesetRepository(MemoryDatabase database)
    {
        this.database = database;
    }

    public async Task<bool> CreateNodeset(string name)
    {
        var nodeset = new Nodeset
        {
            Name = name,
            Nodes = new Dictionary<Guid, Node>()
        };
        await Task.Delay(50); // Pretend to do work :)
        return database.Data.TryAdd(name, nodeset);
    }

    public async Task<bool> DeleteNodeset(string name)
    {
        await Task.Delay(50); // Pretend to do work :)
        return database.Data.Remove(name);
    }

    public async Task<bool> EditNodeset(string name, string newName)
    {
        await Task.Delay(50); // Pretend to do work :)
        var found = database.Data.TryGetValue(name, out Nodeset? nodeset);
        if (found)
        {
            nodeset!.Name = newName;
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Nodeset>> GetAllNodesets()
    {
        await Task.Delay(50); // Pretend to do work :)
        return database.Data.Values.ToList();
    }

    public async Task<Nodeset?> GetNodeset(string name)
    {
        await Task.Delay(50); // Pretend to do work :)
        return database.Data.GetValueOrDefault(name);
    }
}