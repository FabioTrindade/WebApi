using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Abstracts;
using WebApi.Ecommerce.Domain.Commands;
using WebApi.Ecommerce.Domain.DTOs;
using WebApi.Ecommerce.Domain.Entities;
using WebApi.Ecommerce.Domain.Repositories;
using WebApi.Ecommerce.Infra.Contexts;

namespace WebApi.Ecommerce.Infra.Repositories
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly WebApiDataContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly NpgsqlConnection _connection;
        private readonly ILogErroRepository _logErroRepository;

        public EntityRepository(WebApiDataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public EntityRepository(WebApiDataContext context, ILogErroRepository logErroRepository)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _logErroRepository = logErroRepository;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _logErroRepository.CreateAsync(new LogErro(method: string.Concat("UPDATE: ", this.GetType().Name), path: typeof(TEntity).FullName, erro: ex.Message, erroCompleto: ex.ToString(), query: _dbSet.ToQueryString()));
            }

            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _logErroRepository.CreateAsync(new LogErro(method: string.Concat("DELETE: ", this.GetType().Name), path: typeof(TEntity).FullName, erro: ex.Message, erroCompleto: ex.ToString(), query: _dbSet.ToQueryString()));
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate)?.FirstOrDefaultAsync();
        }

        public virtual async Task<List<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            var list = Activator.CreateInstance<List<T>>();
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                var result = await _connection.QueryAsync<T>(sql, parameters);
                if (result != null)
                    list.AddRange(result);
            }
            catch (Exception ex)
            {
                await _logErroRepository.CreateAsync(new LogErro(method: string.Concat("QUERY ASYNC: ", this.GetType().Name), path: typeof(TEntity).FullName, erro: ex.Message, erroCompleto: ex.ToString(), query: sql));
                throw new Exception(sql, ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return list.ToList();
        }

        public virtual async Task<T> QueryFirstAsync<T>(string sql, object parameters = null)
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                return await _connection.QueryFirstAsync<T>(sql, parameters);
            }
            catch (Exception ex)
            {
                await _logErroRepository.CreateAsync(new LogErro(method: string.Concat("QUERY FIRST ASYNC: ", this.GetType().Name), path: typeof(TEntity).FullName, erro: ex.Message, erroCompleto: ex.ToString(), query: sql));
                throw new Exception(sql, ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public virtual async Task<BootstrapTablePaginationDTO<PaginatedEntity>> QueryPaginatedAsync<PaginatedEntity>(string sql, BootstrapTableCommand filter, object parameters = null) where PaginatedEntity : Paginated
        {
            ValidateQueryPagination(sql);

            try
            {
                sql += $" ORDER BY {filter.Sort} {filter.Order}";
                sql += $" LIMIT {filter.Limit} OFFSET {filter.Offset}";

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                var result = await _connection.QueryAsync<PaginatedEntity>(sql, parameters);

                return new BootstrapTablePaginationDTO<PaginatedEntity>
                {
                    Rows = result.ToList(),
                    Total = result.FirstOrDefault()?.Total ?? 0
                };
            }
            catch (Exception ex)
            {
                await _logErroRepository.CreateAsync(new LogErro(method: string.Concat("QUERY PAGINATED: ", this.GetType().Name), path: typeof(TEntity).FullName, erro: ex.Message, erroCompleto: ex.ToString(), query: sql));
                throw new Exception(sql, ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        private void ValidateQueryPagination(string sql)
        {
            if (!sql.Contains("COUNT(*) OVER() AS TOTAL", StringComparison.InvariantCultureIgnoreCase))
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
