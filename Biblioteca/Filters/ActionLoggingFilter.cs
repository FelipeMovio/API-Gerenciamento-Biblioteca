using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Biblioteca.Api.Filters;

public sealed class ActionLoggingFilter : IAsyncActionFilter
{
    private readonly ILogger<ActionLoggingFilter> _logger;

    public ActionLoggingFilter(ILogger<ActionLoggingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(
    ActionExecutingContext context,
    ActionExecutionDelegate next)
    {
        var controller = context.Controller.GetType().Name;
        var action = context.ActionDescriptor.RouteValues["action"];

        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "Action iniciada: {Controller}.{Action}",
            controller,
            action);

        try
        {
            var executedContext = await next();

            if (executedContext.Exception is not null &&
                !executedContext.ExceptionHandled)
            {
                _logger.LogWarning(
                    "Action terminou com exceção: {Controller}.{Action}",
                    controller,
                    action);
            }
        }
        finally
        {
            stopwatch.Stop();

            _logger.LogInformation(
                "Action finalizada: {Controller}.{Action} | Duração: {ElapsedMilliseconds} ms",
                controller,
                action,
                stopwatch.ElapsedMilliseconds);
        }
    }
}