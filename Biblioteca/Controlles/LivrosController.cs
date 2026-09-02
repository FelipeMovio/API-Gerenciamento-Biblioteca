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
    public IActionResult createLivro
        ([FromBody] CreateLivroDto livroDto)
    {
       var livro = _livrosService.CreateLivro(livroDto);

        if (livro == null)
        {
            return NotFound();
        }

        return Ok(livro);

        
    }
}
