using Biblioteca.Domain.Models.LivroMod;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.LivroOps.Queries;

public sealed record ObterLivroPorIdQuery(int Id)
    : IRequest<ObterLivroPorIdQuery.Response?>
{
    public sealed class Handler(
        ILivroRepository livroRepository)
        : IRequestHandler<ObterLivroPorIdQuery, Response?>
    {
        public async Task<Response?> Handle(
            ObterLivroPorIdQuery request,
            CancellationToken cancellationToken)
        {
            var livro = await livroRepository.SelecionarAsync(
                selector: l => new Response(
                    l.Id,
                    l.Titulo,
                    l.Autor,
                    l.ISBN,
                    l.AnoPublicacao,
                    l.CategoriaId,
                    l.Disponivel,
                    l.Categoria != null
                        ? l.Categoria.Nome
                        : string.Empty),
                predicate: l => l.Id == request.Id,
                cancellationToken: cancellationToken);

            return livro;
        }
    }

    public sealed record Response(
        int Id,
        string Titulo,
        string Autor,
        string ISBN,
        int AnoPublicacao,
        int CategoriaId,
        bool Disponivel,
        string CategoriaNome);

    public sealed class Validator
        : AbstractValidator<ObterLivroPorIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID do livro deve ser maior que zero.");
        }
    }
}