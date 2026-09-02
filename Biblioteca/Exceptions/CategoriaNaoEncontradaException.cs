namespace Biblioteca.Exceptions;

public class CategoriaNaoEncontradaException : Exception
{
    public CategoriaNaoEncontradaException(int categoriaId)
        : base($"A categoria com ID {categoriaId} não foi encontrada.")
    {
    }
}