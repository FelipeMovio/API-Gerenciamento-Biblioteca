
namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class CodigoIinvalidoException : Exception
{
    public CodigoIinvalidoException()
        : base("Código de validação inválido ou expirado.")
    {}
}
