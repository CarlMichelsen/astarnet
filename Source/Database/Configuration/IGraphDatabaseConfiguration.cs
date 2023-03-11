namespace Astar.Database.Configuration;

/// <summary>
/// Configuration interface for Neo4j graph database repositories.
/// </summary>
public interface IGraphDatabaseConfiguration
{
    /// <summary>
    /// Gets uri for neo4j database.
    /// </summary>
    /// <value>String uri.</value>
    string Uri { get; }

    /// <summary>
    /// Gets user for neo4j database.
    /// </summary>
    /// <value>String user.</value>
    string User { get; }

    /// <summary>
    /// Gets password for neo4j database.
    /// </summary>
    /// <value>String password.</value>
    string Password { get; }
}
