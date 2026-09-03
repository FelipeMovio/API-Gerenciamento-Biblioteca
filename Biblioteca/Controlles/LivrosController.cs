using Biblioteca.Dtos;
using Biblioteca.Exceptions;
using Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controlles;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly LivrosService _livrosService;

    public LivrosController(LivrosService service)
    {
        this._livrosService = service;
    }

    [HttpPost]
    public IActionResult CreateLivro
        ([FromBody] CreateLivroDto livroDto)
    {
        try
        {
            var livro = _livrosService.CreateLivro(livroDto);

            if (livro == null)
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(GetLivroById),
                new { id = livro.Id },
                livro);
        }
        catch (CategoriaNaoEncontradaException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }
    [HttpGet]
    public ActionResult<List<ReadLivroDto>> GetLivros()
    {
        List<ReadLivroDto> livros = _livrosService.GetLivros();

        if(livros == null)
        {
            return NotFound();
        }

        return livros;
    }

    [HttpGet("{id}")]
    public IActionResult GetLivroById(int id)
    {
        ReadLivroDto? livro = _livrosService.GetLivroById(id);

        if (livro == null)
        {
            return NotFound();

        }

        return Ok(livro);


    }

    [HttpPut("{id}")]
    public IActionResult UpdateLivro(int id,
        [FromBody] UpdateLivroDto updateLivroDto)
    {
        try
        {
            bool livroDto = _livrosService.UpdateLivro(id, updateLivroDto);

            if (!livroDto)
            {
                return NotFound();
            }

            return NoContent();

        }
        catch (CategoriaNaoEncontradaException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    } 
    [HttpDelete("{id}")]
    public IActionResult DeleteLivro(int id)
    {
        bool livroDto = _livrosService.DeleteLivro(id);

        if (!livroDto)
        {
            return NotFound();
        }

        return NoContent();
    }
}
