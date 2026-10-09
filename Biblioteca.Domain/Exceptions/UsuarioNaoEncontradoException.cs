
namespace Biblioteca.Domain.Exceptions;

public sealed class UsuarioNaoEncontradoException : Exception
{
    public UsuarioNaoEncontradoException()
        : base("Usuário não encontrado.")
    {
    }

    public UsuarioNaoEncontradoException(int usuarioId)
        : base($"O usuário com ID {usuarioId} não foi encontrado.")
    {
    }
}