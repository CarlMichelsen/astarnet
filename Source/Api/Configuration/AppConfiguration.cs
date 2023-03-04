using Services.Configuration;

namespace Api.Configuration;

public class AppConfiguration : IDiscordConfiguration
{
    private readonly IConfiguration configuration;

    public AppConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string WebhookUrl => configuration["Discord:WebhookUrl"] ?? throw new NullReferenceException();

    public string Username => configuration["Discord:Username"] ?? throw new NullReferenceException();
}