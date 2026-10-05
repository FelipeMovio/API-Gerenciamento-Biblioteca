using Biblioteca.Domain.Models.UsuarioMod;
using Biblioteca.Domain.Models.UsuarioMod.Enum;
using MediatR;

namespace Biblioteca.Application.Modules.UsuarioOps.Queries;

public sealed record ObterUsuarioPorIdQuery(int Id)
    : IRequest<ObterUsuarioPorIdQuery.Response?>
{
    public sealed class Handler(
        IUsuarioRepository usuarioRepository)
        : IRequestHandler<ObterUsuarioPorIdQuery, Response?>
    {
        public async Task<Response?> Handle(
            ObterUsuarioPorIdQuery request,
            CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.ObterPorIdAsync(
                request.Id,
                cancellationToken);

            if (usuario is null)
                return null;

            return new Response(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.EmailConfirmado,
                usuario.Tipo,
                usuario.DataCadastro);
        }
    }

    public sealed record Response(
        int Id,
        string Nome,
        string Email,
        bool EmailConfirmado,
        TipoUsuario Tipo,
        DateTime DataCadastro);
}