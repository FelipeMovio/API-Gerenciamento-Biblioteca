using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Domain.Models.LivroMod;
using Biblioteca.Domain.Models.UsuarioMod;
using MediatR;

namespace Biblioteca.Application.Modules.EmprestimoOps.Commands;

public sealed record CriarEmprestimoCommand(int LivroId,int UsuarioId,DateTime DataDevolucaoPrevista) 
    : IRequest<CriarEmprestimoCommand.ResultadoCriacao>
{
    public sealed class Handler(
        IUsuarioRepository usuarioRepository,
        ILivroRepository livroRepository,
        IEmprestimoRepository emprestimoRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CriarEmprestimoCommand, ResultadoCriacao>
    {
        public async Task<ResultadoCriacao> Handle(
            CriarEmprestimoCommand request,
            CancellationToken cancellationToken)
        {
            // Regra de negócio será implementada aqui.
            Usuario? usuario = await usuarioRepository.ObterPorIdAsync(request.UsuarioId,cancellationToken);
            if(usuario == null)
            {
                return ResultadoCriacao.UsuarioNaoEncontrado;
            }

            Livro? livro = await livroRepository.ObterPorIdAsync(request.LivroId,cancellationToken);
            if (livro == null)
            {
                return ResultadoCriacao.LivroNaoEncontrado;
            }
            if (!livro.Disponivel)
            {
                return ResultadoCriacao.LivroIndisponivel;
            }
            DateTime dataEmprestimo = DateTime.UtcNow;

            Emprestimo emprestimo = new(
                request.LivroId,
                request.UsuarioId,
                dataEmprestimo,
                request.DataDevolucaoPrevista);

            livro.MarcarComoIndisponivel();
            emprestimoRepository.Adicionar(emprestimo);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultadoCriacao.Criado;
        }
    }

    public enum ResultadoCriacao
    {
        Criado,
        UsuarioNaoEncontrado,
        LivroNaoEncontrado,
        LivroIndisponivel
    }
}