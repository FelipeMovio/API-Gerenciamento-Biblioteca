using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.CategoriaMod.Exceptions;

public class CategoriaNaoExisteException : NotFoundException
{
    public CategoriaNaoExisteException()
        : base($"A categoria com ID não foi encontrada.")
    {
    }

}