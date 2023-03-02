namespace Api.Middleware;

public class RootMiddleware : IMiddleware
{
    private readonly ILogger<RootMiddleware> logger;

    public RootMiddleware(ILogger<RootMiddleware> logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        logger.LogInformation("[{}] {}", context.Request.Method, context.Request.Path);
        await next(context);
    }
}