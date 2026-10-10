
using Biblioteca.Domain.Exceptions;
using System.Text.Json;

namespace Biblioteca.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await TratarExcecaoAsync(context, exception);
        }
    }


    private static async Task TratarExcecaoAsync(
        HttpContext context,
        Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            LivroNaoEncontradoException
                => (StatusCodes.Status404NotFound, exception.Message),

            UsuarioNaoEncontradoException
                => (StatusCodes.Status404NotFound, exception.Message),

            EmprestimoNaoEncontradoException
                => (StatusCodes.Status404NotFound, exception.Message),

            LivroIndisponivelException
                => (StatusCodes.Status409Conflict, exception.Message),

            EmprestimoJaDevolvidoException
                => (StatusCodes.Status409Conflict, exception.Message),

            ArgumentException
                => (StatusCodes.Status400BadRequest, exception.Message),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Ocorreu um erro interno no servidor.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var resposta = new
        {
            status = statusCode,
            message,
            timestamp = DateTime.UtcNow
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(resposta));
    }
}