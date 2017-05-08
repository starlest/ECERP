namespace ECERP.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Abstract;
    using Core;
    using Microsoft.EntityFrameworkCore;

    public class EntityFrameworkReadOnlyRepository<TContext> : IReadOnlyRepository
        where TContext : DbContext
    {
        protected readonly TContext context;

        protected EntityFrameworkReadOnlyRepository(TContext context)
        {
            this.context = context;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                query = orderBy(query);


            if (skip.HasValue)
                query = query.Skip(skip.Value);


            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQueryable(null, orderBy, skip, take, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return await GetQueryable(null, orderBy, skip, take, includeProperties).ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter, orderBy, skip, take, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return await GetQueryable(filter, orderBy, skip, take, includeProperties).ToListAsync();
        }

        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter, null, null, null, includeProperties).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return await GetQueryable(filter, null, null, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter, orderBy, null, null, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return await GetQueryable(filter, orderBy, null, null, includeProperties).FirstOrDefaultAsync();
        }

        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class, IEntity
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual TEntity GetById<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQueryable(x => x.Id.Equals(id), null, null, null, includeProperties).SingleOrDefault();
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class, IEntity
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable(filter).AnyAsync();
        }
    }
}