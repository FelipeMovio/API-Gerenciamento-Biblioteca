using Biblioteca.Domain._Core.Base;

using Biblioteca.Infrastructure.Modulos._Core.Context;

namespace Biblioteca.Infrastructure._Core.Base;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}