namespace Biblioteca.Domain.Models.CategoriaMod.Exceptions;

public sealed class CategoriaPossuiLivroExistenteException : Exception
{
    public CategoriaPossuiLivroExistenteException()
        : base("Categoria ainda possui livro existente.")
    {
    }
}