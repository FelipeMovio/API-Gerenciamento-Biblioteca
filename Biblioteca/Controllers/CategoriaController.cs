using Biblioteca.Application.Modules.CategoriaOps.Commands;
using Biblioteca.Application.Modules.CategoriaOps.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriaController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriaController(ISender sender)
    {
        _sender = sender;
    }

    // GET: /api/categoria
    [HttpGet]
    public async Task<IActionResult> GetCategorias(
        CancellationToken cancellationToken)
    {
        var categorias = await _sender.Send(
            new ObterCategoriasQuery(),
            cancellationToken);

        return Ok(categorias);
    }

    // GET: /api/categoria/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoriaPorId(
        int id,
        CancellationToken cancellationToken)
    {
        var categoria = await _sender.Send(
            new ObterCategoriaPorIdQuery(id),
            cancellationToken);

        return Ok(categoria);
    }

    // POST: /api/categoria
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CriarCategoria(
        [FromBody] CriarCategoriaCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetCategoriaPorId),
            new { id },
            null);
    }

    // PUT: /api/categoria/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AtualizarCategoria(
        int id,
        [FromBody] AtualizarCategoriaCommand command,
        CancellationToken cancellationToken)
    {
        var commandComId = command with { Id = id };

        await _sender.Send(
            commandComId,
            cancellationToken);

        return NoContent();
    }

    // DELETE: /api/categoria/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ExcluirCategoria(
        int id,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new ExcluirCategoriaCommand(id),
            cancellationToken);

        return NoContent();
    }
}