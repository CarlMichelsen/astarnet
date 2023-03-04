namespace Services.Interface;

public interface IDiscordLog
{
    Task LogToDiscord(Guid identifier, string logMessage, string? detailedLogMessage);
}