

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class CredenciaisInvalidasException : Exception
{
    public CredenciaisInvalidasException()
    : base("SCredenciais invalidas")
    {
    }
}
