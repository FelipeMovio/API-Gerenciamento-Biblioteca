
namespace Biblioteca.Domain.Exceptions;

public sealed class EmprestimoJaDevolvidoException : Exception
{
    public EmprestimoJaDevolvidoException()
        : base("Este empréstimo já foi devolvido.")
    {
    }

    public EmprestimoJaDevolvidoException(int emprestimoId)
        : base($"O empréstimo com ID {emprestimoId} já foi devolvido.")
    {
    }
}