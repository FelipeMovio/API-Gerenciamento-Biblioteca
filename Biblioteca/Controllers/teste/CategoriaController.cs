using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers.teste;

public class CategoriaController : ControllerBase
{
    public IActionResult Index()
    {
        return View();
    }
}
