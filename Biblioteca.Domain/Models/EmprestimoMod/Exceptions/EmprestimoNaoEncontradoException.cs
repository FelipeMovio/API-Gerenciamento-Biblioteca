using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.EmprestimoMod.Exceptions;

public sealed class EmprestimoNaoEncontradoException : NotFoundException
{
    public EmprestimoNaoEncontradoException()
        : base("Empréstimo não encontrado.")
    {
    }

    public EmprestimoNaoEncontradoException(int emprestimoId)
        : base($"O empréstimo com ID {emprestimoId} não foi encontrado.")
    {
    }
}