
namespace Biblioteca.Domain.Exceptions;

public sealed class LivroNaoEncontradoException : Exception
{
    public LivroNaoEncontradoException()
        : base("Livro não encontrado.")
    {
    }

    public LivroNaoEncontradoException(int livroId)
        : base($"O livro com ID {livroId} não foi encontrado.")
    {
    }
}