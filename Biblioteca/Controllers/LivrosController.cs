
using Biblioteca.Application.Modules.LivroOps.Commands;
using Biblioteca.Application.Modules.LivroOps.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly ISender _sender;

    public LivrosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLivro([FromBody] CriarLivroCommand command,
    CancellationToken cancellationToken)
    {
        var livro = await _sender.Send(
            command,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            livro);
    }
}
