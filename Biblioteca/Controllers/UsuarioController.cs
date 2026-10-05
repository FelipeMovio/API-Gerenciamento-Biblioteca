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

    [HttpGet("me")]
    public async Task<IActionResult> MeuPerfil(
        CancellationToken cancellationToken)
    {
        var usuarioId = User.FindFirst("sub")?.Value;

        if (!int.TryParse(usuarioId, out var id))
            return Unauthorized();

        var usuario = await _sender.Send(
            new ObterUsuarioPorIdQuery(id),
            cancellationToken);

        if (usuario is null)
            return NotFound();

        return Ok(usuario);
    }
}

