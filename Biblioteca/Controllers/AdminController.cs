using Biblioteca.Application.Modules.UsuarioOps.Commands;
using Biblioteca.Application.Modules.UsuarioOps.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]/usuario")]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly ISender _sender;

    public AdminController(ISender sender)
    {
        _sender = sender;
    }

    // GET: /api/admin/usuario
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetUsuarios(
        CancellationToken cancellationToken)
    {
        var usuarios = await _sender.Send(
            new ObterUsuariosQuery(),
            cancellationToken);

        return Ok(usuarios);
    }

    // GET: /api/admin/usuario/{id}
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetUsuarioPorId(
        int id,
        CancellationToken cancellationToken)
    {
        var usuario = await _sender.Send(
            new ObterUsuarioPorIdQuery(id),
            cancellationToken);

        return Ok(usuario);
    }

    // PUT: /api/admin/usuario/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AtualizarUsuario(
        int id,
        [FromBody] AtualizarUsuarioCommand command,
        CancellationToken cancellationToken)
    {
        var commandComId = command with { Id = id };

        await _sender.Send(
            commandComId,
            cancellationToken);

        return NoContent();
    }

    // DELETE: /api/admin/usuario/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ExcluirUsuario(
        int id,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new ExcluirUsuarioCommand(id),
            cancellationToken);

        return NoContent();
    }
}
