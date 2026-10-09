using Biblioteca.Domain.Models.EmprestimoMod;
using MediatR;

namespace Biblioteca.Application.Modules.EmprestimoOps.Queries;

public sealed record ObterEmprestimoPorIdQuery(int Id)
    : IRequest<ObterEmprestimoPorIdQuery.Response?>
{
    public sealed class Handler(
        IEmprestimoRepository emprestimoRepository)
        : IRequestHandler<ObterEmprestimoPorIdQuery, Response?>
    {
        public async Task<Response?> Handle(
            ObterEmprestimoPorIdQuery request,
            CancellationToken cancellationToken)
        {
            Emprestimo? emprestimo = await emprestimoRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (emprestimo == null)
                return null;

            return new Response(
                emprestimo.Id,
                emprestimo.LivroId,
                emprestimo.Livro?.Titulo,
                emprestimo.UsuarioId,
                emprestimo.Usuario?.Nome,
                emprestimo.DataEmprestimo,
                emprestimo.DataDevolucaoPrevista,
                emprestimo.DataDevolucao,
                emprestimo.EstaDevolvido());
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