using Biblioteca.Domain.Models.UsuarioMod;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Application.Modules.UsuarioOps.Commands;

public sealed record LoginUsuarioCommand(
    string Email,
    string Password)
    : IRequest<LoginUsuarioCommand.Response?>
{
    public sealed class Handler(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher<Usuario> passwordHasher)
        : IRequestHandler<LoginUsuarioCommand, Response?>
    {
        public async Task<Response?> Handle(
            LoginUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var usuario = await usuarioRepository.SelecionarAsync(
                usuario => usuario,
                usuario => usuario.Email == email,
                cancellationToken);

            if (usuario is null)
                return null;

            var resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.PasswordHash,
                request.Password);

            if (resultado == PasswordVerificationResult.Failed)
                return null;

            return new Response(
                usuario.Id,
                usuario.Nome,
                usuario.Email);
        }
    }

    public sealed record Response(
        int Id,
        string Nome,
        string Email);

    public sealed class Validator
        : AbstractValidator<LoginUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("Informe um e-mail válido.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha é obrigatória.");
        }
    }
}