using Biblioteca.Domain.Models.EmprestimoMod;
using MediatR;

namespace Biblioteca.Application.Modules.EmprestimoOps.Queries;

public sealed record ObterEmprestimosPorUsuarioQuery(int UsuarioId)
    : IRequest<IEnumerable<ObterEmprestimosPorUsuarioQuery.Response>>
{
    public sealed class Handler(
        IEmprestimoRepository emprestimoRepository)
        : IRequestHandler<
            ObterEmprestimosPorUsuarioQuery,
            IEnumerable<Response>>
    {
        public async Task<IEnumerable<Response>> Handle(
            ObterEmprestimosPorUsuarioQuery request,
            CancellationToken cancellationToken)
        {
            return await emprestimoRepository.ListarAsync(
                emprestimo => new Response(
                    emprestimo.Id,
                    emprestimo.LivroId,
                    emprestimo.Livro != null
                        ? emprestimo.Livro.Titulo
                        : null,
                    emprestimo.DataEmprestimo,
                    emprestimo.DataDevolucaoPrevista,
                    emprestimo.DataDevolucao,
                    emprestimo.EstaDevolvido()),
                emprestimo => emprestimo.UsuarioId == request.UsuarioId,
                cancellationToken: cancellationToken);
        }
    }

    public sealed record Response(
        int Id,
        int LivroId,
        string? TituloLivro,
        DateTime DataEmprestimo,
        DateTime DataDevolucaoPrevista,
        DateTime? DataDevolucao,
        bool Devolvido);
}