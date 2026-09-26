using Biblioteca.Application.Modules.UsuarioOps.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register( [FromBody] CriarUsuarioCommand command,CancellationToken cancellationToken)
    {
        var usuario = await sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(Register),
            new { id = usuario.Id },
            usuario);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
    [FromBody] LoginUsuarioCommand command,
    CancellationToken cancellationToken)
    {
        var usuario = await sender.Send(
            command,
            cancellationToken);

        if (usuario is null)
            return Unauthorized(new
            {
                message = "E-mail ou senha inválidos."
            });

        return Ok(usuario);
    }
}