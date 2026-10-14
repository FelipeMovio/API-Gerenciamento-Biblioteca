

using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class CredenciaisInvalidasException : UnauthorizedException
{
    public CredenciaisInvalidasException()
    : base("SCredenciais invalidas")
    {
    }
}
