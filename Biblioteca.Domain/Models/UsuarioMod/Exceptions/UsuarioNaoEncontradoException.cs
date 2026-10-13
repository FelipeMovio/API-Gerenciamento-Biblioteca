using Biblioteca.Domain._Core.ExceptionBase;

namespace Biblioteca.Domain.Models.UsuarioMod.Exceptions;

public sealed class UsuarioNaoEncontradoException : NotFoundException
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