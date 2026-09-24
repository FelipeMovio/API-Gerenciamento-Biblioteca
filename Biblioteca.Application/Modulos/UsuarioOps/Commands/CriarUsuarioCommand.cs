using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.UsuarioMod;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Biblioteca.Application.Modules.UsuarioOps.Commands;

public sealed record CriarUsuarioCommand(
    string Nome,
    string Email,
    string Password)
    : IRequest<CriarUsuarioCommand.Response>
{
    public sealed class Handler(
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher<Usuario> passwordHasher)
        : IRequestHandler<CriarUsuarioCommand, Response>
    {
        public async Task<Response> Handle(
            CriarUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            var emailJaExiste = await usuarioRepository.ExisteAsync(
                usuario => usuario.Email == request.Email,
                cancellationToken);

            if (emailJaExiste)
                throw new InvalidOperationException(
                    "Já existe um usuário cadastrado com este e-mail.");

            var usuario = new Usuario
            {
                Nome = request.Nome.Trim(),
                Email = request.Email.Trim().ToLowerInvariant()
            };

            usuario.PasswordHash = passwordHasher.HashPassword(
                usuario,
                request.Password);

            usuarioRepository.Adicionar(usuario);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new Response(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.DataCadastro);
        }
    }

    public sealed record Response(
        int Id,
        string Nome,
        string Email,
        DateTime DataCadastro);

    public sealed class Validator
        : AbstractValidator<CriarUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MinimumLength(2)
                .WithMessage("O nome deve ter pelo menos 2 caracteres.")
                .MaximumLength(150)
                .WithMessage("O nome deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("Informe um e-mail válido.")
                .MaximumLength(200)
                .WithMessage("O e-mail deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("A senha é obrigatória.")
                .MinimumLength(8)
                .WithMessage("A senha deve ter pelo menos 8 caracteres.")
                .MaximumLength(100)
                .WithMessage("A senha deve ter no máximo 100 caracteres.");
        }
    }
}