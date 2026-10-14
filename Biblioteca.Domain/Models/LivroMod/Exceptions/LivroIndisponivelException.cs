using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.LivroMod.Exceptions;

public sealed class LivroIndisponivelException : ConflictException
{
    public LivroIndisponivelException()
        : base("O livro não está disponível para empréstimo.")
    {
    }

    public LivroIndisponivelException(int livroId)
        : base($"O livro com ID {livroId} não está disponível para empréstimo.")
    {
    }
}