using Astar.Database.Configuration;
using Neo4j.Driver;

namespace Astar.Database.Repositories;

/// <summary>
/// BaseGraphRepository class that contains client initialization and a EnsureClientConnected method for access to that client.
/// </summary>
public abstract class BaseGraphRepository : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseGraphRepository"/> class.
    /// </summary>
    /// <param name="config">Neo4j graph database configuration.</param>
    public BaseGraphRepository(IGraphDatabaseConfiguration config)
    {
        this.Client = GraphDatabase.Driver(config.Uri, AuthTokens.Basic(config.User, config.Password));
    }

    /// <summary>
    /// Gets IDriver client.
    /// </summary>
    /// <value>IDriver interface.</value>
    public IDriver Client { get; private set; }

    /// <summary>
    /// IDisposible implementation.
    /// </summary>
    public void Dispose()
    {
        this.Client.Dispose();
        GC.SuppressFinalize(this);
    }
}