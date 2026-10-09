
namespace Biblioteca.Domain.Exceptions;

public sealed class EmprestimoNaoEncontradoException : Exception
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