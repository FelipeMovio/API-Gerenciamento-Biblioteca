using Biblioteca.Domain.Models.UsuarioMod;
using Biblioteca.Domain.Models.UsuarioMod.Enum;
using MediatR;

namespace Biblioteca.Application.Modules.UsuarioOps.Queries;

public sealed record ObterUsuariosQuery()
    : IRequest<IEnumerable<ObterUsuariosQuery.Response>>
{
    public sealed class Handler(
        IUsuarioRepository usuarioRepository)
        : IRequestHandler<ObterUsuariosQuery, IEnumerable<Response>>
    {
        public async Task<IEnumerable<Response>> Handle(
            ObterUsuariosQuery request,
            CancellationToken cancellationToken)
        {
            IEnumerable<Response> usuarios = await usuarioRepository.ListarAsync(
                usuario => new Response(
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.EmailConfirmado,
                    usuario.Tipo,
                    usuario.DataCadastro),
                cancellationToken: cancellationToken);

            return usuarios;
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