using Astar.Services.Configuration;

namespace Astar.Api.Configuration;

/// <summary>
/// Configuration file for the Api and it's dependencies.
/// </summary>
public class AppConfiguration : IDiscordConfiguration
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

    /// <summary>
    /// Gets the WebhookUrl for discord logging.
    /// </summary>
    /// <returns>A string url.</returns>
    public string WebhookUrl => this.configuration["Discord:WebhookUrl"] ?? throw new NullReferenceException();

    /// <summary>
    /// Gets the discordusername used for logging uncaught exceptions in a discord server.
    /// </summary>
    /// <returns>String username.</returns>
    public string Username => this.configuration["Discord:Username"] ?? throw new NullReferenceException();
}