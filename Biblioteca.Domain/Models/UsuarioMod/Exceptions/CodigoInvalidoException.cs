
namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class CodigoInvalidoException : Exception
{
    public CodigoInvalidoException()
        : base("Código de validação inválido ou expirado.")
    {}
}
