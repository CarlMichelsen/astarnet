using Astar.Database.Configuration;
using Astar.Database.Entities;
using Astar.Database.Repositories.Interface;

namespace Astar.Database.Repositories;

/// <summary>
/// Graph database INodeRepository implementation.
/// </summary>
public class GraphNodeRepository : BaseGraphRepository, INodeRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GraphNodeRepository"/> class.
    /// </summary>
    /// <param name="config">Neo4j graph database configuration.</param>
    public GraphNodeRepository(IGraphDatabaseConfiguration config)
        : base(config)
    {
    }

    /// <inheritdoc />
    public Task<IEnumerable<Node>> CreateNodes(string nodesetName, IEnumerable<Node> nodes)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Node>> DeleteNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Node>> EditNodes(string nodesetName, IEnumerable<Node> nodes)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Node>> GetNodes(string nodesetName, IEnumerable<Guid> guids)
    {
        throw new NotImplementedException();
    }
}