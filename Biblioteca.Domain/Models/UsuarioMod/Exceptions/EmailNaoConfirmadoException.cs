

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class EmailNaoConfirmadoException : Exception
{
    public EmailNaoConfirmadoException(string email)
    : base($"Email {email} nao confirmado ainda")
    {
    }
}
