namespace Astar.Services.Interface;

/// <summary>
/// Interface for service that sends discord messages.
/// </summary>
public interface IDiscordLog
{
    /// <summary>
    /// Send message, usually a logmessage, to a discord webhook so it shows up on a discord server.
    /// </summary>
    /// <param name="identifier">Stacktrace identifier for log-messages.</param>
    /// <param name="logMessage">Actualt logmessage.</param>
    /// <returns>Just a task to keep track of progress.</returns>
    Task LogToDiscord(Guid identifier, string logMessage);
}