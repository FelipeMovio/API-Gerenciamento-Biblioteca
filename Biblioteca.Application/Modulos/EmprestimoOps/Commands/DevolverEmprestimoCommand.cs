using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Exceptions;
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

            if (emprestimo == null)
            {
                throw new EmprestimoNaoEncontradoException(request.EmprestimoId);
            }

            if (emprestimo.EstaDevolvido())
            {
                throw new EmprestimoJaDevolvidoException(emprestimo.Id);
            }

            Livro? livro =
                await livroRepository.ObterPorIdAsync(
                    emprestimo.LivroId,
                    cancellationToken);

            if (livro == null)
            {
                throw new LivroNaoEncontradoException(emprestimo.LivroId);
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