using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Domain.Models.LivroMod;
using FluentValidation;
using MediatR;

namespace Biblioteca.Application.Modules.EmprestimoOps.Commands;

public sealed record DevolverEmprestimoCommand(
    int EmprestimoId) : IRequest<DevolverEmprestimoCommand.ResultadoDevolucao>
{
    public sealed class Handler(
        IEmprestimoRepository emprestimoRepository,
        ILivroRepository livroRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<DevolverEmprestimoCommand, ResultadoDevolucao>
    {
        public async Task<ResultadoDevolucao> Handle(
            DevolverEmprestimoCommand request,
            CancellationToken cancellationToken)
        {
            Emprestimo? emprestimo =
                await emprestimoRepository.ObterPorIdAsync(
                    request.EmprestimoId,
                    cancellationToken);

            if (emprestimo is null)
            {
                return ResultadoDevolucao.EmprestimoNaoEncontrado;
            }

            if (emprestimo.EstaDevolvido())
            {
                return ResultadoDevolucao.EmprestimoJaDevolvido;
            }

            Livro? livro =
                await livroRepository.ObterPorIdAsync(
                    emprestimo.LivroId,
                    cancellationToken);

            if (livro is null)
            {
                return ResultadoDevolucao.LivroNaoEncontrado;
            }

            emprestimo.RegistrarDevolucao(DateTime.UtcNow);

            livro.MarcarComoDisponivel();

            emprestimoRepository.Atualizar(emprestimo);
            livroRepository.Atualizar(livro);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResultadoDevolucao.Devolvido;
        }
    }

    public enum ResultadoDevolucao
    {
        Devolvido,
        EmprestimoNaoEncontrado,
        EmprestimoJaDevolvido,
        LivroNaoEncontrado
    }

    public sealed class DevolverEmprestimoCommandValidator : AbstractValidator<DevolverEmprestimoCommand>
    {
        public DevolverEmprestimoCommandValidator()
        {
            RuleFor(x => x.EmprestimoId)
                .GreaterThan(0)
                .WithMessage("O ID do empréstimo deve ser maior que zero.");
        }
    }
}