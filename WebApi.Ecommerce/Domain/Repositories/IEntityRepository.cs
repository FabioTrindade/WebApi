using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface IEntityRepository<TEntity> : IDisposable where TEntity : Entity, new()
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task Delete(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetPaginationAsync(string sql, object parameters = null);
    }
}
