using Biblioteca.Application.Modules.LivroOps.Commands;
using Biblioteca.Application.Modules.LivroOps.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LivrosController : ControllerBase
{
    private readonly ISender _sender;

    public LivrosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CreateLivro([FromBody] CriarLivroCommand command,
    CancellationToken cancellationToken)
    {
        var livro = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetLivroById),
            new { id = livro.Id },
            livro); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLivroById( int id, CancellationToken cancellationToken)
    {
        var livro = await _sender.Send( new ObterLivroPorIdQuery(id),cancellationToken);

        if (livro is null)
            return NotFound();

        return Ok(livro);
    }

    [HttpGet]
    public async Task<IActionResult> GetLivros(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new ObterLivrosQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> UpdateLivro(int id,[FromBody] AtualizarLivroCommand command,CancellationToken cancellationToken)
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
    public async Task<IActionResult> DeleteLivro(int id,CancellationToken cancellationToken)
    {
        var removido = await _sender.Send(new ExcluirLivroCommand(id),cancellationToken);

        if (!removido)
            return NotFound();

        return NoContent();
    }
}
