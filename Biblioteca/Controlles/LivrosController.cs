using Biblioteca.Dtos;
using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controlles;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : Controller
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
       var livro = _livrosService.CreateLivro(livroDto);

        if (livro == null)
        {
            return NotFound();
        }

        return Ok(livro);
    }

    [HttpGet]
    public IEnumerable<ReadLivroDto> GetLivros
        ([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        List<ReadLivroDto> livros = _livrosService.GetLivros();

        if(livros == null)
        {
            return (IEnumerable<ReadLivroDto>)NotFound();
        }

        return livros;
    }

    [HttpGet("{id}")]
    public IActionResult GetLivroById(int id)
    {
        ReadLivroDto livro = _livrosService.GetLivroById(id);

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
        bool livroDto = _livrosService.UpdateLivro(id, updateLivroDto);

        if (!livroDto)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLivro(int id)
    {
        bool livroDto = _livrosService.DeleteLivro(id);

        if (!livroDto)
        {
            return BadRequest();
        }

        return Ok();
    }
}
