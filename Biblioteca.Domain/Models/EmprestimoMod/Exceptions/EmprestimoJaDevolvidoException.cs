using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.EmprestimoMod.Exceptions;

public sealed class EmprestimoJaDevolvidoException : ConflictException
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