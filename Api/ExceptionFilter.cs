using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api;

public class ExceptionFilter : IAsyncActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // do nothing
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            await next(); // Execute the action
        }
        catch (Exception ex)
        {
            // Log the exception or handle it in some way
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(ActionExecutingContext context, Exception ex)
    {
        // handle the exception here
        context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        await Task.CompletedTask;
    }
}