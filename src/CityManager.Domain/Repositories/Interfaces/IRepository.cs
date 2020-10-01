using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CityManager.Domain.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Remove(TEntity obj);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}