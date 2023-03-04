using System.Net.Http.Headers;
using System.Text.Json;
using Services.Configuration;
using Services.Interface;

namespace Services;

public class DiscordLog : IDiscordLog
{
    private readonly HttpClient client;
    private readonly IDiscordConfiguration config;

    public DiscordLog(HttpClient client, IDiscordConfiguration config)
    {
        this.client = client;
        this.config = config;
    }

    public async Task LogToDiscord(Guid identifier, string logMessage, string? detailedLogMessage)
    {
        Dictionary<string, string> message = new()
        {
            { "content", FormatDiscordMessage(identifier, logMessage, detailedLogMessage) },
            { "username", config.Username }
        };
        var json = JsonSerializer.Serialize(message);
        var req = new HttpRequestMessage(HttpMethod.Post, new Uri(config.WebhookUrl))
        {
            Content = new StringContent(json, new MediaTypeHeaderValue("application/json")),
        };

        var res = await client.SendAsync(req);
        res.EnsureSuccessStatusCode();
    }

    private static string FormatDiscordMessage(Guid identifier, string logMessage, string? detailedLogMessage)
    {
        var formattedMessage = string.Empty;
        if (!string.IsNullOrWhiteSpace(detailedLogMessage)) formattedMessage = $"\n```{detailedLogMessage}```";
        return $"{identifier}\n*{logMessage}*{formattedMessage}";
    }
}