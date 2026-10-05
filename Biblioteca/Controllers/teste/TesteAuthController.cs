using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers.teste;

[ApiController]
[Route("api/teste-auth")]
public class TesteAuthController : ControllerBase
{
    [HttpGet("publico")]
    public IActionResult Publico()
    {
        return Ok(new
        {
            message = "Endpoint público."
        });
    }

    [Authorize]
    [HttpGet("protegido")]
    public IActionResult Protegido()
    {
        return Ok(new
        {
            message = "Você está autenticado!",
            usuario = User.Identity?.Name,
            email = User.FindFirst("email")?.Value
        });
    }
}