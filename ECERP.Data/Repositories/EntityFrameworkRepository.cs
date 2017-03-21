namespace ECERP.Data.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Abstract;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EntityFrameworkRepository<TContext> : EntityFrameworkReadOnlyRepository<TContext>, IRepository
        where TContext : DbContext
    {
        public EntityFrameworkRepository(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy)
            where TEntity : class, IEntity
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = createdBy;
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy)
            where TEntity : class, IEntity
        {
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = modifiedBy;
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            var entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }
    }
}