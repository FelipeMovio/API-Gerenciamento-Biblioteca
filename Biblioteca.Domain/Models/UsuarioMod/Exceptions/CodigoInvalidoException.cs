
using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public class CodigoInvalidoException : BadRequestException
{
    public CodigoInvalidoException()
        : base("Código de validação inválido ou expirado.")
    {}
}
