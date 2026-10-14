using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.UsuarioMod;
using Biblioteca.Domain.Models.UsuarioMod.Exceptions;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.UsuarioOps.Commands;

public sealed record ExcluirUsuarioCommand(int Id)
    : IRequest<bool>
{
    public sealed class Handler(
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<ExcluirUsuarioCommand, bool>
    {
        public async Task<bool> Handle(
            ExcluirUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            Usuario? usuario = await usuarioRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (usuario == null)
                throw new UsuarioNaoEncontradoException(request.Id);

            usuarioRepository.Remover(usuario);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
    public sealed class Validator
    : AbstractValidator<ExcluirUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID do usuário deve ser maior que zero.");
        }
    }
}