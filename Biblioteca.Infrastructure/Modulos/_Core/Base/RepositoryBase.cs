using Biblioteca.Domain._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Biblioteca.Infrastructure._Core.Base;

public abstract class RepositoryBase<TEntity>(AppDbContext dbContext)
    : IBaseRepository<TEntity>
    where TEntity : class
{
    protected AppDbContext DbContext { get; } = dbContext;

    protected DbSet<TEntity> Set { get; } = dbContext.Set<TEntity>();


    // Consultas e leituras

    public IQueryable<TEntity> QueryAsNoTracking()
        => Set.AsNoTracking();


    public async Task<bool> ExisteAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Set
            .AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
    }


    public async Task<TEntity?> ObterPorIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Set.FindAsync([id], cancellationToken);
    }


    public async Task<TResult?> SelecionarAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Set.AsNoTracking();

        if (predicate is not null)
            query = query.Where(predicate);

        return await query
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<IEnumerable<TResult>> ListarAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Set.AsNoTracking();

        if (predicate is not null)
            query = query.Where(predicate);

        return await query
            .Select(selector)
            .ToListAsync(cancellationToken);
    }


    // Operações de escrita

    public void Adicionar(TEntity entity)
        => Set.Add(entity);


    public void AdicionarRange(IEnumerable<TEntity> entities)
        => Set.AddRange(entities);


    public void Atualizar(TEntity entity)
        => Set.Update(entity);


    public void Remover(TEntity entity)
        => Set.Remove(entity);


    public void RemoverRange(IEnumerable<TEntity> entities)
        => Set.RemoveRange(entities);
}