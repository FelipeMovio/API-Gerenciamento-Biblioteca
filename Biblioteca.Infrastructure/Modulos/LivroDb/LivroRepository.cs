
using Biblioteca.Domain.Models.LivroMod;
using Biblioteca.Infrastructure._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;

using Microsoft.EntityFrameworkCore;


namespace Biblioteca.Infrastructure.Modulos.LivroDb;

public class LivroRepository(AppDbContext dbContext)
    : RepositoryBase<Livro>(dbContext),
      ILivroRepository
{

    public async Task<IEnumerable<Livro>> GetLivro()
    {
        return await dbContext.Livros.ToListAsync();
    }
}
