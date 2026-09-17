

using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Infrastructure._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Modulos.CategoriaDb;

public class CategoriaRepository(AppDbContext dbContext)
    : RepositoryBase<Categoria>(dbContext),
      ICategoriaRepository
{

    public async Task<IEnumerable<Categoria>> GetCategorias()
    {
        return await dbContext.Categorias.ToListAsync();
    }
}
