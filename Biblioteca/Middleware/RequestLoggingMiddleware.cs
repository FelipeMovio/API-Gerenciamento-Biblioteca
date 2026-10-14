
using System.Diagnostics;

namespace Biblioteca.Api.Middleware;

public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.LogInformation(
            "Requisição iniciada: {Method} {Path}",
            method,
            path);

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var statusCode = context.Response.StatusCode;
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation(
                "Requisição finalizada: {Method} {Path} | Status: {StatusCode} | Duração: {ElapsedMilliseconds} ms",
                method,
                path,
                statusCode,
                elapsedMilliseconds);
        }
    }
}