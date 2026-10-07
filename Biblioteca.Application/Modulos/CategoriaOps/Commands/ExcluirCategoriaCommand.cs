using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.LivroMod;
using MediatR;
using static Biblioteca.Application.Modules.CategoriaOps.Commands.ExcluirCategoriaCommand;

namespace Biblioteca.Application.Modules.CategoriaOps.Commands;

public sealed record ExcluirCategoriaCommand(int Id) : IRequest<ResultadoExclusao>
{
    public sealed class Handler(
        ICategoriaRepository categoriaRepository,
        ILivroRepository livroRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<ExcluirCategoriaCommand, ResultadoExclusao>
    {
        public async Task<ResultadoExclusao> Handle(
            ExcluirCategoriaCommand request,
            CancellationToken cancellationToken)
        {
            Categoria? categoria = await categoriaRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (categoria is null)
                return ResultadoExclusao.NaoEncontrada;

            bool possuiLivros = await livroRepository.ExisteAsync(
                livro => livro.CategoriaId == request.Id,
                cancellationToken);

            if (possuiLivros)
                return ResultadoExclusao.PossuiLivros;

            categoriaRepository.Remover(categoria);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultadoExclusao.Excluida;
        }
    }

    public enum ResultadoExclusao
    {
        Excluida,
        NaoEncontrada,
        PossuiLivros
    }
}