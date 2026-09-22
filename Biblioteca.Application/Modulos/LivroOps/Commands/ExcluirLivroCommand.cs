using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.LivroMod;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.LivroOps.Commands;

public sealed record ExcluirLivroCommand(int Id)
    : IRequest<bool>
{
    public sealed class Handler(
        ILivroRepository livroRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<ExcluirLivroCommand, bool>
    {
        public async Task<bool> Handle(
            ExcluirLivroCommand request,
            CancellationToken cancellationToken)
        {
            var livro = await livroRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (livro is null)
                return false;

            livroRepository.Remover(livro);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public sealed class Validator
        : AbstractValidator<ExcluirLivroCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID do livro deve ser maior que zero.");
        }
    }
}