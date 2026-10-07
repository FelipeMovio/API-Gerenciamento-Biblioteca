using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Infrastructure._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
