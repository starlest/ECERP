using System;

namespace ECERP.Data.Tests
{
    using Abstract;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Entities;
    using Repositories;

    public class DatabaseFixture : IDisposable
    {
        #region Fields
        private IServiceProvider _serviceProvider;
        private ECERPDbContext _dbContext;
        #endregion

        public DatabaseFixture()
        {
            var services = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<ECERPDbContext>(options => options.UseInMemoryDatabase("Scratch"));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ECERPDbContext>();
            _serviceProvider = services.BuildServiceProvider();
            _dbContext = _serviceProvider.GetRequiredService<ECERPDbContext>();
            Repository = new EntityFrameworkRepository<ECERPDbContext>(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository Repository { get; }
    }
}