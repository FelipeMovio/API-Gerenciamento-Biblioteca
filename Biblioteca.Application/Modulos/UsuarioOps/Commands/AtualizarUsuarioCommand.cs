using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.UsuarioMod;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.UsuarioOps.Commands;

public sealed record AtualizarUsuarioCommand(
    int Id,
    string Nome,
    string Email)
    : IRequest<bool>
{
    public sealed class Handler(
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<AtualizarUsuarioCommand, bool>
    {
        public async Task<bool> Handle(
            AtualizarUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (usuario is null)
                return false;

            string email = request.Email.Trim().ToLowerInvariant();

            bool emailJaExiste = await usuarioRepository.ExisteAsync(
                u => u.Email == email && u.Id != request.Id,
                cancellationToken);

            if (emailJaExiste)
                throw new InvalidOperationException(
                    "Já existe um usuário cadastrado com este e-mail.");

            usuario.AlterarNome(request.Nome);
            usuario.AlterarEmail(email);

            usuarioRepository.Atualizar(usuario);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public sealed class Validator
        : AbstractValidator<AtualizarUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID do usuário deve ser maior que zero.");

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
        }
    }
}