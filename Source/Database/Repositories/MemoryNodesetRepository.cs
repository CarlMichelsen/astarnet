using Astar.Database.Entities;
using Astar.Database.Repositories.Interface;

namespace Astar.Database.Repositories;

/// <summary>
/// Implementation of INodesetRepository.
/// </summary>
public class MemoryNodesetRepository : INodesetRepository
{
    private readonly MemoryDatabase database;

    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryNodesetRepository"/> class.
    /// </summary>
    /// <param name="database">In-memory database for testing.</param>
    public MemoryNodesetRepository(MemoryDatabase database)
    {
        this.database = database;
    }

    /// <inheritdoc />
    public async Task<bool> CreateNodeset(string name)
    {
        var nodeset = new Nodeset
        {
            Name = name,
            Nodes = new Dictionary<Guid, Node>(),
        };
        await Task.Delay(10); // Pretend to do work :)
        return this.database.Data.TryAdd(name, nodeset);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteNodeset(string name)
    {
        await Task.Delay(10); // Pretend to do work :)
        return this.database.Data.Remove(name);
    }

    /// <inheritdoc />
    public async Task<bool> EditNodeset(string name, string newName)
    {
        await Task.Delay(10); // Pretend to do work :)
        var found = this.database.Data.TryGetValue(name, out Nodeset? nodeset);
        if (found)
        {
            nodeset!.Name = newName;
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<string>> GetAllNodesets()
    {
        await Task.Delay(10); // Pretend to do work :)
        return this.database.Data.Keys.ToList();
    }

    /// <inheritdoc />
    public async Task<Nodeset?> GetNodeset(string name)
    {
        await Task.Delay(10); // Pretend to do work :)
        return this.database.Data.GetValueOrDefault(name);
    }
}