using Astar.Database.Configuration;
using Astar.Database.Entities;
using Astar.Database.Repositories.Interface;
using Neo4j.Driver;

namespace Astar.Database.Repositories;

/// <summary>
/// Graph database INodesetRepository implementation.
/// </summary>
public class GraphNodesetRepository : BaseGraphRepository, INodesetRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GraphNodesetRepository"/> class.
    /// </summary>
    /// <param name="config">Neo4j graph database configuration.</param>
    public GraphNodesetRepository(IGraphDatabaseConfiguration config)
        : base(config)
    {
    }

    /// <inheritdoc />
    public Task<bool> CreateNodeset(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> DeleteNodeset(string name)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> EditNodeset(string name, string newName)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<string>> GetAllNodesets()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Nodeset?> GetNodeset(string name)
    {
        throw new NotImplementedException();
    }
}