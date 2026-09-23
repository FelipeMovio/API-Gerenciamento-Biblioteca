using System.Linq.Expressions;

namespace Biblioteca.Domain._Core.Base;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    // Consultas e leituras

    IQueryable<TEntity> QueryAsNoTracking();

    Task<bool> ExisteAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<TEntity?> ObterPorIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<TResult?> SelecionarAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TResult>> ListarAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);


    // Operações de escrita

    void Adicionar(TEntity entity);

    void AdicionarRange(IEnumerable<TEntity> entities);

    void Atualizar(TEntity entity);

    void Remover(TEntity entity);

    void RemoverRange(IEnumerable<TEntity> entities);
}