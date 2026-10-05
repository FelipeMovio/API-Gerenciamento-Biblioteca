using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.LivroMod;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.LivroOps.Commands;

public sealed record AtualizarLivroCommand(
    int Id,
    string Titulo,
    string Autor,
    string ISBN,
    int AnoPublicacao,
    int CategoriaId,
    bool Disponivel)
    : IRequest<bool>
{
    public sealed class Handler(
        ILivroRepository livroRepository,
        ICategoriaRepository categoriaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<AtualizarLivroCommand, bool>
    {
        public async Task<bool> Handle(
            AtualizarLivroCommand request,
            CancellationToken cancellationToken)
        {
            var livro = await livroRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (livro is null)
                return false;

            var categoria = await categoriaRepository.ObterPorIdAsync(
                request.CategoriaId,
                cancellationToken);

            if (categoria is null)
                throw new Exception(
                    $"A categoria com ID {request.CategoriaId} não foi encontrada.");

            livro.AlterarDados(
                request.Titulo,
                request.Autor,
                request.ISBN,
                request.AnoPublicacao,
                categoria.Id);

            if (request.Disponivel)
                livro.MarcarComoDisponivel();
            else
                livro.MarcarComoIndisponivel();

            livroRepository.Atualizar(livro);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public sealed class Validator
        : AbstractValidator<AtualizarLivroCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID do livro deve ser maior que zero.");

            RuleFor(x => x.Titulo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O título do livro é obrigatório.")
                .MinimumLength(2)
                .WithMessage("O título deve ter pelo menos 2 caracteres.")
                .MaximumLength(200)
                .WithMessage("O título deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Autor)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O autor é obrigatório.")
                .MinimumLength(2)
                .WithMessage("O autor deve ter pelo menos 2 caracteres.")
                .MaximumLength(150)
                .WithMessage("O autor deve ter no máximo 150 caracteres.");

            RuleFor(x => x.ISBN)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O ISBN é obrigatório.")
                .Length(10, 13)
                .WithMessage("O ISBN deve ter entre 10 e 13 caracteres.");

            RuleFor(x => x.AnoPublicacao)
                .InclusiveBetween(1000, 2100)
                .WithMessage(
                    "O ano de publicação deve estar entre 1000 e 2100.");

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0)
                .WithMessage("A categoria é obrigatória.");
        }
    }
}