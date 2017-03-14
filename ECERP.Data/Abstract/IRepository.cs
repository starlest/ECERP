namespace ECERP.Data.Abstract
{
    using System.Threading.Tasks;
    using Models;
    using Models.Entities;

    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, ApplicationUser createdBy) where TEntity : class, IEntity;
        void Update<TEntity>(TEntity entity, ApplicationUser modifiedBy) where TEntity : class, IEntity;
        void Delete<TEntity>(object id) where TEntity : class, IEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Save();
        Task SaveAsync();
    }
}