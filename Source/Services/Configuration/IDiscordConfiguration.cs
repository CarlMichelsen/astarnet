namespace Astar.Services.Configuration;

/// <summary>
/// Configuration for DiscordLog service.
/// </summary>
public interface IDiscordConfiguration
{
    /// <summary>
    /// Gets the username for the discord message.
    /// </summary>
    /// <value>String username value.</value>
    string Username { get; }

    /// <summary>
    /// Gets WebhookUrl to send discordmessages.
    /// </summary>
    /// <value>String WebhookUrl.</value>
    string WebhookUrl { get; }
}