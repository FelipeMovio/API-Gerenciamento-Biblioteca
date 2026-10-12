
using Biblioteca.Domain.Models.CategoriaMod.Exceptions;
using Biblioteca.Domain.Models.EmprestimoMod.Exceptions;
using Biblioteca.Domain.Models.LivroMod.Exceptions;
using Biblioteca.Domain.Models.UsuarioMod.Exceptions;
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
            // 404 - Recurso não encontrado
            LivroNaoEncontradoException
                => (StatusCodes.Status404NotFound, exception.Message),

            UsuarioNaoEncontradoException
                => (StatusCodes.Status404NotFound, exception.Message),

            EmprestimoNaoEncontradoException
                => (StatusCodes.Status404NotFound, exception.Message),

            CategoriaNaoEncontradaException
                => (StatusCodes.Status404NotFound, exception.Message),

            // 409 - Conflito com o estado atual do recurso
            LivroIndisponivelException
                => (StatusCodes.Status409Conflict, exception.Message),

            EmprestimoJaDevolvidoException
                => (StatusCodes.Status409Conflict, exception.Message),

            CategoriaPossuiLivroExistenteException
                => (StatusCodes.Status409Conflict, exception.Message),

            JaExisteUsuarioComEmailException
                => (StatusCodes.Status409Conflict, exception.Message),

            // 401 - Falha de autenticação
            CredenciaisInvalidasException
                => (StatusCodes.Status401Unauthorized, exception.Message),

            // 400 - Argumentos inválidos
            ArgumentException
                => (StatusCodes.Status400BadRequest, exception.Message),

            // 500 - Erro inesperado
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