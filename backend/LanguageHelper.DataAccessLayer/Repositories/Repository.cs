using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LanguageHelper.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LanguageHelper.DataAccessLayer.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly LanguageHelperDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(LanguageHelperDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public Task CreateManyAsync(ICollection<TEntity> items) => DbSet.AddRangeAsync(items);

        public async Task<TEntity> GetAsync(TKey id) => await DbSet.FindAsync(id); 

        public async Task<List<TEntity>> GetRangeAsync(int index = 1,
                                                       int count = 10,
                                                       Expression<Func<TEntity, bool>> filter = null,
                                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                       bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking) query = query.AsNoTracking();
            if (filter != null) query = query.Where(filter);
            if (include != null) query = include(query);

            query = orderBy == null ? query.OrderBy(a => a.Id) : orderBy(query);

            if (index == 0) index = 1;
            if (count == 0) count = 10;

            return await query.Skip((index - 1) * count).Take(count).ToListAsync();
        }
        
        public TEntity Update(TEntity entity) => DbSet.Update(entity).Entity;

        public async Task DeleteAsync(TKey id) => Delete(await GetAsync(id));

        public void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var entitiesToDelete = await GetRangeAsync(count: int.MaxValue,filter: predicate, include: include, disableTracking: false);
            DbSet.RemoveRange(entitiesToDelete);
        }
    }
}
