namespace ECERP.Data.Tests
{
    using System;
    using Abstract;
    using Core;
    using Core.Domain;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Repositories;

    public class PersistenceTest : IDisposable
    {
        private readonly IRepository _repository;
        private readonly ECERPDbContext _dbContext;

        protected PersistenceTest()
        {
            var services = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<ECERPDbContext>(options => options.UseInMemoryDatabase("Scratch"));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ECERPDbContext>();
            var serviceProvider = services.BuildServiceProvider();
            _dbContext = serviceProvider.GetRequiredService<ECERPDbContext>();
            _repository = new EntityFrameworkRepository<ECERPDbContext>(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        /// <summary>
        /// Persistence test helper
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        protected T SaveAndLoadEntity<T>(T entity) where T : class, IEntity
        {
            _repository.Create(entity);
            var fromDb = _repository.GetById<T>(entity.Id);
            return fromDb;
        }
    }
}