using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.CategoriaMod;
using MediatR;

namespace Biblioteca.Application.Modules.CategoriaOps.Commands;

public sealed record CriarCategoriaCommand(
    string Nome) : IRequest<int>
{
    public sealed class Handler(
        ICategoriaRepository categoriaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CriarCategoriaCommand, int>
    {
        public async Task<int> Handle(
            CriarCategoriaCommand request,
            CancellationToken cancellationToken)
        {
            var categoria = new Categoria(request.Nome);

            categoriaRepository.Adicionar(categoria);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return categoria.Id;
        }
    }
}