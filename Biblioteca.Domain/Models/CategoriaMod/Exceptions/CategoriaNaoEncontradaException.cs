using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.CategoriaMod.Exceptions;

public class CategoriaNaoEncontradaException : NotFoundException
{
    public CategoriaNaoEncontradaException(int categoriaId)
        : base($"A categoria com ID {categoriaId} não foi encontrada.")
    {
    }

    public CategoriaNaoEncontradaException()
    : base($"A categoria com ID não foi encontrada.")
    {
    }
}