using System.Net.Http.Headers;
using System.Text.Json;
using Astar.Services.Configuration;
using Astar.Services.Interface;

namespace Astar.Services;

/// <summary>
/// DiscordLog service that can send messages to a discord server.
/// </summary>
public class DiscordLog : IDiscordLog
{
    private readonly HttpClient client;
    private readonly IDiscordConfiguration config;

    /// <summary>
    /// Initializes a new instance of the <see cref="DiscordLog"/> class.
    /// </summary>
    /// <param name="client">HttpClient to connect to discord api.</param>
    /// <param name="config">Configuration that contains discord-username and discord webhook url.</param>
    public DiscordLog(HttpClient client, IDiscordConfiguration config)
    {
        this.client = client;
        this.config = config;
    }

    /// <inheritdoc />
    public async Task LogToDiscord(Guid identifier, string logMessage)
    {
        Dictionary<string, string> message = new()
        {
            { "content", FormatDiscordMessage(identifier, logMessage) },
            { "username", this.config.Username },
        };
        var json = JsonSerializer.Serialize(message);
        var req = new HttpRequestMessage(HttpMethod.Post, new Uri(this.config.WebhookUrl))
        {
            Content = new StringContent(json, new MediaTypeHeaderValue("application/json")),
        };

        var res = await this.client.SendAsync(req);
        res.EnsureSuccessStatusCode();
    }

    private static string FormatDiscordMessage(Guid identifier, string logMessage)
    {
        return $"*{identifier}*\n**{logMessage}**";
    }
}