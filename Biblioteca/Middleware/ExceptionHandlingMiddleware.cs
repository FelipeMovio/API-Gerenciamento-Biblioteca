using Biblioteca.Domain._Core.ExceptionBase;
using System.Text.Json;

namespace Biblioteca.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Erro não tratado durante a requisição {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await TratarExcecaoAsync(context, exception);
        }
    }

    private static async Task TratarExcecaoAsync(
        HttpContext context,
        Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            BadRequestException
                => (StatusCodes.Status400BadRequest, exception.Message),

            UnauthorizedException
                => (StatusCodes.Status401Unauthorized, exception.Message),

            ForbiddenException
                => (StatusCodes.Status403Forbidden, exception.Message),

            NotFoundException
                => (StatusCodes.Status404NotFound, exception.Message),

            MethodNotAllowedException
                => (StatusCodes.Status405MethodNotAllowed, exception.Message),

            ConflictException
                => (StatusCodes.Status409Conflict, exception.Message),

            GoneException
                => (StatusCodes.Status410Gone, exception.Message),

            UnprocessableEntityException
                => (StatusCodes.Status422UnprocessableEntity, exception.Message),

            TooManyRequestsException
                => (StatusCodes.Status429TooManyRequests, exception.Message),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Ocorreu um erro interno no servidor.")
        };

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        context.Response.Headers["Cache-Control"] = "no-store";

        var resposta = new
        {
            status = statusCode,
            message,
            timestamp = DateTimeOffset.UtcNow
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(resposta));
    }
}