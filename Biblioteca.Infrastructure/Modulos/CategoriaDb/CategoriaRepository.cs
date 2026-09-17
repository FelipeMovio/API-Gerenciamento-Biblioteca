

using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Infrastructure.Modulos._Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Modulos.CategoriaDb;

public class CategoriaRepository(AppDbContext dbContext)
{

    public async Task<IEnumerable<Categoria>> GetCategorias()
    {
        return await dbContext.Categorias.ToListAsync();
    }
}
