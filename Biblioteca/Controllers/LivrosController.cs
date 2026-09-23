
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
}
