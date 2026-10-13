using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public sealed class JaExisteUsuarioComEmailException : ConflictException
{
    public JaExisteUsuarioComEmailException()
        : base("Usuário com email ja existe.")
    {
    }

    public JaExisteUsuarioComEmailException(string usuarioEmail)
        : base($"O usuário com ID {usuarioEmail} Ja existe.")
    {
    }
}