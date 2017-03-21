namespace ECERP.Data.Abstract
{
    using System.Threading.Tasks;
    using Models;

    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy) where TEntity : class, IEntity;
        void Update<TEntity>(TEntity entity, string modifiedBy) where TEntity : class, IEntity;
        void Delete<TEntity>(object id) where TEntity : class, IEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Save();
        Task SaveAsync();
    }
}