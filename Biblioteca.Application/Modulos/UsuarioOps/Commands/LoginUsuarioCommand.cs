using Biblioteca.Application.Security;
using Biblioteca.Domain.Models.UsuarioMod;
using Biblioteca.Domain.Models.UsuarioMod.Enum;
using Biblioteca.Domain.Models.UsuarioMod.Exceptions;
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
        IPasswordHasher<Usuario> passwordHasher,
        IJwtService jwtService)
        : IRequestHandler<LoginUsuarioCommand, Response?>
    {
        public async Task<Response?> Handle(
            LoginUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            string email = request.Email.Trim().ToLowerInvariant();

            Usuario? usuario = await usuarioRepository.SelecionarAsync(
                usuario => usuario,
                usuario => usuario.Email == email,
                cancellationToken);

            if (usuario == null)
            {
                throw new CredenciaisInvalidasException();
            }

            PasswordVerificationResult resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.PasswordHash,
                request.Password);

            if (resultado == PasswordVerificationResult.Failed)
            {
                throw new CredenciaisInvalidasException();
            }

            if(!usuario.EmailConfirmado)
            {
                throw new EmailNaoConfirmadoException(email);
            }

            string token = jwtService.GerarToken(
                usuario.Id, usuario.Nome, usuario.Email,usuario.Tipo);

            return new Response(
                usuario.Id, usuario.Nome,
                usuario.Email,usuario.Tipo,token);
        }
    }

    public sealed record Response(
        int Id,
        string Nome,
        string Email,
        TipoUsuario Tipo,
        string Token);

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