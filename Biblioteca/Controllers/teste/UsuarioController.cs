using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Api.Controllers.teste;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController(ISender _sender) : ControllerBase
{
}
