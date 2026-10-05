using Biblioteca.Application.Modules.UsuarioOps.Dtos;
using Biblioteca.Application.Notifications;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Application.Modules.UsuarioOps.Commands;

public sealed record EnviarCodigoEmailCommand(
    string Email)
    : IRequest<EnviarCodigoEmailCommand.Response>
{
    public sealed class Handler(
        IMemoryCache memoryCache,
        IEmailService emailService)
        : IRequestHandler<EnviarCodigoEmailCommand, Response>
    {
        public async Task<Response> Handle(
            EnviarCodigoEmailCommand request,
            CancellationToken cancellationToken)
        {
            // Gera um código aleatório de 6 dígitos.
            var codigoGerado = RandomNumberGenerator
                .GetInt32(100000, 1000000)
                .ToString();

            // Identifica essa solicitação de validação.
            var Id = Guid.NewGuid().ToString();

            // Salva apenas o hash do código no cache.
            var codigoHash = GerarHash(codigoGerado);

            // O código será válido por 3 minutos.
            var dataExpiracao = DateTimeOffset.UtcNow.AddMinutes(3);

            var dadosValidacao = new ValidacaoEmailDto(
                request.Email,
                codigoHash,
                0,
                dataExpiracao);

            // Usa o TransacaoId como chave do cache.
            memoryCache.Set(
                Id,
                dadosValidacao,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = dataExpiracao
                });

            // Monta o e-mail.
            var mensagem = $"""
                Seu código de validação é: {codigoGerado}

                O código expira em 3 minutos.
                """;

            // Envia o código para o e-mail informado.
            await emailService.EnviarEmailAsync(
                request.Email,
                "Código de validação - Biblioteca API",
                mensagem);

            return new Response(
                true,
                "Código enviado com sucesso.",
                Id);
        }

        private static string GerarHash(string input)
        {
            using var sha256 = SHA256.Create();

            var bytes = sha256.ComputeHash(
                Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(bytes);
        }
    }

    public sealed record Response(
        bool Success,
        string Message,
        string? Id);

    public sealed class Validator
        : AbstractValidator<EnviarCodigoEmailCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("O e-mail informado é inválido.");
        }
    }
}