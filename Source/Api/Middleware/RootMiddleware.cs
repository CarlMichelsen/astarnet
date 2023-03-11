using Astar.Services.Interface;

namespace Astar.Api.Middleware;

/// <summary>
/// The first middleware to be hit when an api-request is made.
/// Behaves as an Exception-interceptor.
/// </summary>
public class RootMiddleware : IMiddleware
{
    private readonly IDiscordLog discordLog;
    private readonly ILogger<RootMiddleware> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RootMiddleware"/> class.
    /// </summary>
    /// <param name="logger">A general logger interface.</param>
    /// <param name="discordLog">A discord logging service for uncaught exceptions.</param>
    public RootMiddleware(ILogger<RootMiddleware> logger, IDiscordLog discordLog)
    {
        this.logger = logger;
        this.discordLog = discordLog;
    }

    /// <summary>
    /// Logs when a request comes through and creates an identifier for the request for tracability.
    /// </summary>
    /// <param name="context">HttpContext of the incoming request.</param>
    /// <param name="next">Delegate function that holds whatever is supposed to run after this middleware.</param>
    /// <returns>Nothing. Just a task to keep track of the async method.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var identifier = Guid.NewGuid();
        try
        {
            this.logger.LogInformation(
                "<{identifier}> [{method}] {path}",
                identifier,
                context.Request.Method,
                context.Request.Path);
            context.TraceIdentifier = identifier.ToString();
            await next(context);
        }
        catch (Exception ex)
        {
            this.logger.LogCritical("<{identifier}> {exception}", identifier, ex);
            await this.discordLog.LogToDiscord(
                identifier,
                ex.Message);
        }
    }
}