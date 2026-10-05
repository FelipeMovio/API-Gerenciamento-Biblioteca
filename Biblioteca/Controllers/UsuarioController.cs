using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController(ISender sender) : ControllerBase
{
    public ISender Sender { get; } = sender;


}
