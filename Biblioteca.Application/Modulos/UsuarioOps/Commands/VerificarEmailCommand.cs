using Biblioteca.Application.Modules.UsuarioOps.Dtos;
using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.UsuarioMod;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Application.Modules.UsuarioOps.Commands;

public sealed record VerificarEmailCommand(
    string Id,
    string Codigo)
    : IRequest<VerificarEmailCommand.Response>
{
    public sealed class Handler(
        IMemoryCache memoryCache,
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<VerificarEmailCommand, Response>
    {
        public async Task<Response> Handle(
            VerificarEmailCommand request,
            CancellationToken cancellationToken)
        {
            // Procura os dados da validação no cache.
            if (!memoryCache.TryGetValue(
                    request.Id,
                    out ValidacaoEmailDto? dadosValidacao)
                || dadosValidacao is null)
            {
                return Response.Fail(
                    "Código de validação inválido ou expirado.");
            }

            // Verifica se o código já expirou.
            if (DateTimeOffset.UtcNow > dadosValidacao.DataExpiracao)
            {
                memoryCache.Remove(request.Id);

                return Response.Fail(
                    "Código de validação expirado.");
            }

            // Gera o hash do código informado pelo usuário.
            var codigoHash = GerarHash(request.Codigo);

            // Compara o hash recebido com o hash armazenado.
            if (!CryptographicOperations.FixedTimeEquals(
                    Convert.FromBase64String(codigoHash),
                    Convert.FromBase64String(dadosValidacao.CodigoHash)))
            {
                return Response.Fail(
                    "Código de validação inválido.");
            }

            // Procura o usuário pelo e-mail associado à transação.
            var usuario = await usuarioRepository.SelecionarAsync(
                usuario => usuario,
                usuario => usuario.Email == dadosValidacao.Email,
                cancellationToken);

            if (usuario is null)
            {
                return Response.Fail(
                    "Usuário não encontrado.");
            }

            // Confirma o e-mail.
            usuario.EmailConfirmado = true;

            usuarioRepository.Atualizar(usuario);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            // O código não pode ser reutilizado.
            memoryCache.Remove(request.Id);

            return Response.Ok(
                "E-mail confirmado com sucesso.");
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
        string Message)
    {
        public static Response Ok(string message)
            => new(true, message);

        public static Response Fail(string message)
            => new(false, message);
    }

    public sealed class Validator
        : AbstractValidator<VerificarEmailCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("A transação é obrigatória.");

            RuleFor(x => x.Codigo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O código é obrigatório.")
                .Matches(@"^\d{6}$")
                .WithMessage("O código deve possuir 6 dígitos.");
        }
    }
}