

using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class EmailNaoConfirmadoException : ForbiddenException
{
    public EmailNaoConfirmadoException(string email)
    : base($"Email {email} nao confirmado ainda")
    {
    }
}
