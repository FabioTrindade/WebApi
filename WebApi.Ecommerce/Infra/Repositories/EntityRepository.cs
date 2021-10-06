using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Abstracts;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly WebApiDataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EntityRepository(WebApiDataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate)?.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPaginationAsync(string sql, object parameters = null)
        {
            ValidateQueryPagination(sql);

           return  await _dbSet.FromSqlRaw<TEntity>(sql, parameters)
                .Skip(0)
                .Take(10)
                .AsNoTracking()
                .ToListAsync();
        }

        private void ValidateQueryPagination(string sql)
        {
            if(sql.Contains("COUNT(*) OVER() TOTAL", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("'COUNT(*) OVER() TOTAL' é obrigatório");
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}
