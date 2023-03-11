using Astar.Database.Configuration;
using Astar.Services.Configuration;

namespace Astar.Api.Configuration;

/// <summary>
/// Configuration file for the Api and it's dependencies.
/// </summary>
public class AppConfiguration : IDiscordConfiguration, IGraphDatabaseConfiguration
{
    private readonly IConfiguration configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfiguration"/> class.
    /// </summary>
    /// <param name="configuration">
    /// Microsoft.Extensions.Configuration.IConfiguration contains configuration variables passed in from whatefter environment the app is running in.
    /// </param>
    public AppConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <inheritdoc />
    public string WebhookUrl => this.configuration["Discord:WebhookUrl"] ?? throw new NullReferenceException();

    /// <inheritdoc />
    public string Username => this.configuration["Discord:Username"] ?? throw new NullReferenceException();

    /// <inheritdoc />
    public string Uri => this.configuration["Neo4j:Uri"] ?? throw new NullReferenceException();

    /// <inheritdoc />
    public string User => this.configuration["Neo4j:User"] ?? throw new NullReferenceException();

    /// <inheritdoc />
    public string Password => this.configuration["Neo4j:Password"] ?? throw new NullReferenceException();
}