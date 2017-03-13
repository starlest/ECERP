namespace ECERP.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Abstract;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>, new()
    {
        private readonly ECERPDbContext _dbContext;

        #region Constructor
        protected Repository(ECERPDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Interface Methods
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }

        public virtual int Count()
        {
            return _dbContext.Set<TEntity>().Count();
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.AsEnumerable();
        }

        public TEntity GetSingle(TKey id)
        {
            return _dbContext.Set<TEntity>().SingleOrDefault(x => x.Id.Equals(id));
        }

        public TEntity GetSingle(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.SingleOrDefault(x => x.Id.Equals(id));
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbContext.Set<TEntity>() as IQueryable<TEntity>;
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query.Where(predicate).SingleOrDefault();
        }

        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var dbEntityEntry = _dbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            var dbEntityEntry = _dbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbContext.Set<TEntity>().Where(predicate);
            foreach (var entity in entities)
                _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            _dbContext.SaveChanges();
        }

        public virtual Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}