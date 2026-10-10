using Biblioteca.Application.Modules.EmprestimoOps.Commands;
using Biblioteca.Application.Modules.EmprestimoOps.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmprestimosController : ControllerBase
{
    private readonly ISender _sender;

    public EmprestimosController(ISender sender)
    {
        _sender = sender;
    }

    // POST: /api/emprestimos
    [HttpPost]
    public async Task<IActionResult> CriarEmprestimo(
        [FromBody] CriarEmprestimoCommand command,
        CancellationToken cancellationToken)
    {
        var usuarioId = ObterUsuarioId();

        if (usuarioId is null)
            return Unauthorized();

        var commandComUsuario = command with { UsuarioId = usuarioId.Value };

        var resultado = await _sender.Send(
            commandComUsuario,
            cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    // PUT: /api/emprestimos/{id}/devolver
    [HttpPut("{id}/devolver")]
    public async Task<IActionResult> DevolverEmprestimo(
        int id,
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new DevolverEmprestimoCommand(id),
            cancellationToken);

        return NoContent();
    }

    // GET: /api/emprestimos/{id}
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetEmprestimoPorId(
        int id,
        CancellationToken cancellationToken)
    {
        var emprestimo = await _sender.Send(
            new ObterEmprestimoPorIdQuery(id),
            cancellationToken);

        if (emprestimo is null)
            return NotFound();

        return Ok(emprestimo);
    }

    // GET: /api/emprestimos
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetEmprestimos(
        CancellationToken cancellationToken)
    {
        var emprestimos = await _sender.Send(
            new ObterEmprestimosQuery(),
            cancellationToken);

        return Ok(emprestimos);
    }

    // GET: /api/emprestimos/me
    [HttpGet("me")]
    public async Task<IActionResult> GetMeusEmprestimos(
        CancellationToken cancellationToken)
    {
        var usuarioId = ObterUsuarioId();

        if (usuarioId is null)
            return Unauthorized();

        var emprestimos = await _sender.Send(
            new ObterEmprestimosPorUsuarioQuery(usuarioId.Value),
            cancellationToken);

        return Ok(emprestimos);
    }

    private int? ObterUsuarioId()
    {
        var usuarioId = User.FindFirst("sub")?.Value;

        return int.TryParse(usuarioId, out var id)
            ? id
            : null;
    }
}