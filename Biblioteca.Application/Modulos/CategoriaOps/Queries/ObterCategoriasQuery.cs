using Biblioteca.Domain.Models.CategoriaMod;
using MediatR;

namespace Biblioteca.Application.Modules.CategoriaOps.Queries;

public sealed record ObterCategoriasQuery
    : IRequest<IEnumerable<ObterCategoriasQuery.Response>>
{
    public sealed class Handler(
        ICategoriaRepository categoriaRepository)
        : IRequestHandler<ObterCategoriasQuery, IEnumerable<Response>>
    {
        public async Task<IEnumerable<Response>> Handle(
            ObterCategoriasQuery request,
            CancellationToken cancellationToken)
        {
            return await categoriaRepository.ListarAsync(
                categoria => new Response(
                    categoria.Id,
                    categoria.Nome),
                cancellationToken: cancellationToken);
        }
    }

    public sealed record Response(
        int Id,
        string Nome);
}