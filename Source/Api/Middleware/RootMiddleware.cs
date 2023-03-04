using Services.Interface;

namespace Api.Middleware;

public class RootMiddleware : IMiddleware
{
    private readonly IDiscordLog discordLog;
    private readonly ILogger<RootMiddleware> logger;

    public RootMiddleware(ILogger<RootMiddleware> logger, IDiscordLog discordLog)
    {
        this.logger = logger;
        this.discordLog = discordLog;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var identifier = Guid.NewGuid();
        try
        {
            logger.LogInformation(
                "<{identifier}> [{method}] {path}",
                identifier,
                context.Request.Method,
                context.Request.Path
            );
            context.TraceIdentifier = identifier.ToString();
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogCritical("<{identifier}> {exception}", identifier, ex);
            var detailedExceptionMessage = ex.InnerException?.ToString();
            await discordLog.LogToDiscord(
                identifier,
                ex.Message,
                detailedExceptionMessage
            );
        }
    }
}