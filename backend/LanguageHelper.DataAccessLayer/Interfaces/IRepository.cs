using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace LanguageHelper.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task CreateManyAsync(ICollection<TEntity> items);
        Task DeleteAsync(TKey id);
        void Delete(TEntity entityToDelete);
        Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> GetAsync(TKey id);
        Task<List<TEntity>> GetRangeAsync(int index = 1,
            int count = 10,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        TEntity Update(TEntity entity);
    }
}
