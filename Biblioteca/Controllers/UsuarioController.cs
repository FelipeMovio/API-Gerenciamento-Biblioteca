using Biblioteca.Application.Modules.UsuarioOps.Commands;
using Biblioteca.Application.Modules.UsuarioOps.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController : ControllerBase
{

    private readonly ISender _sender;

    public UsuarioController(ISender sender)
    {
        _sender = sender;
    }

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


    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetUsuarioPorId(
    int id,
    CancellationToken cancellationToken)
    {
        var usuario = await _sender.Send(
            new ObterUsuarioPorIdQuery(id),
            cancellationToken);

        if (usuario is null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AtualizarUsuario(
    int id,
    [FromBody] AtualizarUsuarioCommand command,
    CancellationToken cancellationToken)
    {
        var commandComId = command with { Id = id };

        var atualizado = await _sender.Send(
            commandComId,
            cancellationToken);

        if (!atualizado)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ExcluirUsuario(
    int id,
    CancellationToken cancellationToken)
    {
        var removido = await _sender.Send(
            new ExcluirUsuarioCommand(id),
            cancellationToken);

        if (!removido)
            return NotFound();

        return NoContent();
    }
}
