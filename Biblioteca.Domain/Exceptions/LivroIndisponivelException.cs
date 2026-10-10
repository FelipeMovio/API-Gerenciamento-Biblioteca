
namespace Biblioteca.Domain.Exceptions;

public sealed class LivroIndisponivelException : Exception
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