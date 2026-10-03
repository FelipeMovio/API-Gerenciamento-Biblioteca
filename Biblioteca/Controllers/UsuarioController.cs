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
}
