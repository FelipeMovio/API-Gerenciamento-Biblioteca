using Biblioteca.Infrastructure.Modulos._Core.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Modulos._Core.Base;

public abstract class RepositoryBase<TEntity>(AppDbContext dbContext) : IBaseRepository<TEntity> where TEntity : class
{
    protected AppDbContext DbContext { get; } = dbContext;

    protected DbSet<TEntity> Set { get; } = dbContext.Set<TEntity>();

    public async Task<bool> ExisteAsync(
    Expression<Func<TEntity, bool>> predicate,
    CancellationToken cancellationToken = default)
    {
        return await Set.AsNoTracking().AnyAsync(predicate, cancellationToken);
    }

    // Expõe a consulta assíncrona/sem rastreamento para leitura rápida
    public IQueryable<TEntity> QueryAsNoTracking()
        => Set.AsNoTracking();

    public async Task<TEntity?> ObterPorIdAsync(dynamic id, CancellationToken cancellationToken = default)
    {
        return await Set.FindAsync(id, cancellationToken);
    }
    // Método de projeção genérico (Select + FirstOrDefault)
    public async Task<TResult?> SelecionarAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Set.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<IEnumerable<TResult>> ListarAsync<TResult>(
    Expression<Func<TEntity, TResult>> selector,
    Expression<Func<TEntity, bool>>? predicate = null,
    CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Set.AsNoTracking();

        if (predicate is not null)
            query = query.Where(predicate);

        return await query.Select(selector).ToListAsync(cancellationToken);
    }
    // Escrita (Operações no Change Tracker)
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

