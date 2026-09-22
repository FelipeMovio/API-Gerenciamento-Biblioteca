using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.LivroMod;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.LivroOps.Queries;

public sealed record ObterLivrosQuery
    : IRequest<ObterLivrosQuery.Response>
{
    public sealed class Handler(
        ILivroRepository livroRepository)
        : IRequestHandler<ObterLivrosQuery, Response>
    {
        public async Task<Response> Handle(
            ObterLivrosQuery request,
            CancellationToken cancellationToken)
        {
            var livros = await livroRepository.ListarAsync(
                selector: livro => new LivroItem(
                    livro.Id,
                    livro.Titulo,
                    livro.Autor,
                    livro.ISBN,
                    livro.AnoPublicacao,
                    livro.CategoriaId,
                    livro.Disponivel,
                    livro.Categoria != null
                        ? livro.Categoria.Nome
                        : string.Empty),
                cancellationToken: cancellationToken);

            return new Response(livros);
        }
    }

    public sealed record Response(
        IEnumerable<LivroItem> Livros);

    public sealed record LivroItem(
        int Id,
        string Titulo,
        string Autor,
        string ISBN,
        int AnoPublicacao,
        int CategoriaId,
        bool Disponivel,
        string CategoriaNome);

    public sealed class Validator
        : AbstractValidator<ObterLivrosQuery>
    {
        public Validator()
        {
        }
    }
}