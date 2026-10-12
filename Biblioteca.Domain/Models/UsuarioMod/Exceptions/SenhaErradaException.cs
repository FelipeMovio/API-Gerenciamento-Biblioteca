

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class SenhaErradaException : Exception
{
    public SenhaErradaException()
    : base("Senha Incorreta.")
    {
    }
}
