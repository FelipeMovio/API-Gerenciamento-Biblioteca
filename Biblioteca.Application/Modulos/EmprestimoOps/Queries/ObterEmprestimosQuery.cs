using Biblioteca.Domain.Models.EmprestimoMod;
using MediatR;

namespace Biblioteca.Application.Modules.EmprestimoOps.Queries;

public sealed record ObterEmprestimosQuery
    : IRequest<IEnumerable<ObterEmprestimosQuery.Response>>
{
    public sealed class Handler(
        IEmprestimoRepository emprestimoRepository)
        : IRequestHandler<ObterEmprestimosQuery, IEnumerable<Response>>
    {
        public async Task<IEnumerable<Response>> Handle(
            ObterEmprestimosQuery request,
            CancellationToken cancellationToken)
        {
            return await emprestimoRepository.ListarAsync(
                emprestimo => new Response(
                    emprestimo.Id,
                    emprestimo.LivroId,
                    emprestimo.Livro!.Titulo,
                    emprestimo.UsuarioId,
                    emprestimo.Usuario!.Nome,
                    emprestimo.DataEmprestimo,
                    emprestimo.DataDevolucaoPrevista,
                    emprestimo.DataDevolucao,
                    emprestimo.EstaDevolvido()),
                cancellationToken: cancellationToken);
        }
    }

    public sealed record Response(
        int Id,
        int LivroId,
        string? TituloLivro,
        int UsuarioId,
        string? NomeUsuario,
        DateTime DataEmprestimo,
        DateTime DataDevolucaoPrevista,
        DateTime? DataDevolucao,
        bool Devolvido);
}