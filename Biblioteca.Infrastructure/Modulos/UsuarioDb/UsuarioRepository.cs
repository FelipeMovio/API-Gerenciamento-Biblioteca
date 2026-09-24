using Biblioteca.Domain.Models.UsuarioMod;
using Biblioteca.Infrastructure._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;

namespace Biblioteca.Infrastructure.Modulos.UsuarioDb;

public class UsuarioRepository(AppDbContext dbContext)
    : RepositoryBase<Usuario>(dbContext), IUsuarioRepository
{
}