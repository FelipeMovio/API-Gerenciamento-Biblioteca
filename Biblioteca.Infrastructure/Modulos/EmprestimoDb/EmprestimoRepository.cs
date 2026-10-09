using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Infrastructure._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Modulos.EmprestimoDb;

public class EmprestimoRepository(AppDbContext dbContext)
    : RepositoryBase<Emprestimo>(dbContext),
      IEmprestimoRepository
{

    public async Task<IEnumerable<Emprestimo>> GetEmprestimos()
    {
        return await dbContext.Emprestimos.ToListAsync();
    }
}
