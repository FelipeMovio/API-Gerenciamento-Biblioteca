using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.CategoriaMod;
using MediatR;

namespace Biblioteca.Application.Modules.CategoriaOps.Commands;

public sealed record AtualizarCategoriaCommand(
    int Id,
    string Nome) : IRequest<bool>
{
    public sealed class Handler(
        ICategoriaRepository categoriaRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<AtualizarCategoriaCommand, bool>
    {
        public async Task<bool> Handle(
            AtualizarCategoriaCommand request,
            CancellationToken cancellationToken)
        {
            var categoria = await categoriaRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (categoria is null)
                return false;

            categoria.AlterarNome(request.Nome);

            categoriaRepository.Atualizar(categoria);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}