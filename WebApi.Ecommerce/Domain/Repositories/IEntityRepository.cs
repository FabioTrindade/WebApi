using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Abstracts;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.DTOs;

namespace WebApi.Ecommerce.Domain.Repositories
{
    public interface IEntityRepository<TEntity> : IDisposable where TEntity : Entity, new()
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task Delete(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<BootstrapTablePaginationDTO<PaginatedEntity>> QueryPaginatedAsync<PaginatedEntity>(string sql, BootstrapTableCommand filter, object parameters = null) where PaginatedEntity : Paginated;
    }
}
