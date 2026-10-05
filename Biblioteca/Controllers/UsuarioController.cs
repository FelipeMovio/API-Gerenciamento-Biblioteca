using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    public IActionResult MeuPerfil()
    {
        var usuarioId = User.FindFirst("sub")?.Value;
        var email = User.FindFirst("email")?.Value;
        var nome = User.FindFirst("name")?.Value;

        return Ok(new
        {
            Id = usuarioId,
            Nome = nome,
            Email = email
        });
    }
}

