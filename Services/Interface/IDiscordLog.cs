namespace Services.Interface;

public interface IDiscordLog
{
    Task LogToDiscord(string identifier, string logMessage, string detailedLogMessage = "");
}