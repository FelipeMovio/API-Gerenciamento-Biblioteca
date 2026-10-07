using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Domain.Models.LivroMod;
using Biblioteca.Domain.Models.UsuarioMod;
using MediatR;

namespace Biblioteca.Application.Modulos.EmprestimoOps.Commands;

public sealed record CriarEmprestimoCommand(
    int LivroId,
    int UsuarioId,
    DateTime DataDevolucaoPrevista) : IRequest<int>
{
    public sealed class Handler(
    IUsuarioRepository usuarioRepository, 
    ILivroRepository livroRepository,
    IEmprestimoRepository emprestimoRepository,
    IUnitOfWork unitOfWork)
        : IRequestHandler<CriarEmprestimoCommand, int>
    {