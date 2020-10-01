using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CityManager.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CityManager.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CityManagerContext _dbContext;
        public BaseRepository(CityManagerContext dbContext) => _dbContext = dbContext;

        public void Add(TEntity obj) => _dbContext.Set<TEntity>().Add(obj);

        public void Remove(TEntity obj) => _dbContext.Set<TEntity>().Remove(obj);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null) => await GetBaseQuery(predicate).ToListAsync();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null) => await GetBaseQuery(predicate).CountAsync();

        private IQueryable<TEntity> GetBaseQuery(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public async Task<TEntity> GetAsync(int id) => await _dbContext.Set<TEntity>().FindAsync(id);

    }
}