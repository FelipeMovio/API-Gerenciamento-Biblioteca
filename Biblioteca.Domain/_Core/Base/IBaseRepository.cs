using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain._Core.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        // Consultas e Leituras
        IQueryable<TEntity> QueryAsNoTracking();

        Task<bool> ExisteAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);
        Task<TEntity?> ObterPorIdAsync(
            dynamic id,
            CancellationToken cancellationToken = default);

        Task<TResult?> SelecionarAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TResult>> ListarAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);

        // Operações de Escrita (Change Tracker / Memória)
        void Adicionar(TEntity entity);

        void AdicionarRange(IEnumerable<TEntity> entities);

        void Atualizar(TEntity entity);

        void Remover(TEntity entity);

        void RemoverRange(IEnumerable<TEntity> entities);
    }
}
