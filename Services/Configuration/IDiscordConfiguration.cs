namespace Services.Configuration;

public interface IDiscordConfiguration
{
    string Username { get; }
    string WebhookUrl { get; }
}