using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.CategoriaMod.Exceptions;
using MediatR;

namespace Biblioteca.Application.Modules.CategoriaOps.Queries;

public sealed record ObterCategoriaPorIdQuery(int Id)
    : IRequest<ObterCategoriaPorIdQuery.Response?>
{
    public sealed class Handler(
        ICategoriaRepository categoriaRepository)
        : IRequestHandler<ObterCategoriaPorIdQuery, Response?>
    {
        public async Task<Response?> Handle(
            ObterCategoriaPorIdQuery request,
            CancellationToken cancellationToken)
        {
            var categoria = await categoriaRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (categoria is null)
                throw new CategoriaNaoEncontradaException();

            return new Response(
                categoria.Id,
                categoria.Nome);
        }
    }

    public sealed record Response(
        int Id,
        string Nome);
}