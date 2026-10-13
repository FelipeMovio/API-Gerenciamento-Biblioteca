using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.CategoriaMod.Exceptions;

public sealed class CategoriaPossuiLivroExistenteException : ConflictException
{
    public CategoriaPossuiLivroExistenteException()
        : base("Categoria ainda possui livro existente.")
    {
    }
}