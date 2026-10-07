
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

    // GET: /api/categorias
    [HttpGet]
    public async Task<IActionResult> GetCategorias(
        CancellationToken cancellationToken)
    {
        var categorias = await _sender.Send(
            new ObterCategoriasQuery(),
            cancellationToken);

        return Ok(categorias);
    }

    // GET: /api/categorias/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoriaPorId(
        int id,
        CancellationToken cancellationToken)
    {
        var categoria = await _sender.Send(
            new ObterCategoriaPorIdQuery(id),
            cancellationToken);

        if (categoria is null)
            return NotFound();

        return Ok(categoria);
    }

    // POST: /api/categorias
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

    // PUT: /api/categorias/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AtualizarCategoria(
        int id,
        [FromBody] AtualizarCategoriaCommand command,
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

    // DELETE: /api/categorias/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> ExcluirCategoria(
        int id,
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ExcluirCategoriaCommand(id),
            cancellationToken);

        return resultado switch
        {
            ExcluirCategoriaCommand.ResultadoExclusao.NaoEncontrada
                => NotFound(),

            ExcluirCategoriaCommand.ResultadoExclusao.PossuiLivros
                => Conflict(new
                {
                    message = "Não é possível excluir a categoria porque existem livros associados a ela."
                }),

            ExcluirCategoriaCommand.ResultadoExclusao.Excluida
                => NoContent(),

            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
