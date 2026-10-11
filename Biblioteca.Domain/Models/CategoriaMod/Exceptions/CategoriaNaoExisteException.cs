namespace Biblioteca.Domain.Models.CategoriaMod.Exceptions;

public class CategoriaNaoExisteException : Exception
{
    public CategoriaNaoExisteException()
        : base($"A categoria com ID não foi encontrada.")
    {
    }

}