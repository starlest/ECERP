namespace ECERP.Business.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abstract;
    using Business.Services;
    using Data;
    using Data.Abstract;
    using Data.Repositories;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Entities;
    using Models.Entities.Companies;
    using Models.Entities.FinancialAccounting;

    public class ServiceFixture : IServiceFixture
    {
        #region Private Members
        private IServiceProvider _serviceProvider;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private ECERPDbContext _dbContext;
        private IRepository _repository;
        #endregion

        #region Constructor
        public ServiceFixture()
        {
            ConfigureServices();
            PopulateMockData();
        }
        #endregion

        #region Interface Properties
        public ApplicationUser Admin { get; private set; }
        public IChartOfAccountsService ChartOfAccountsService { get; private set; }

        public ICompanyService CompanyService { get; private set; }

        public ILedgerAccountService LedgerAccountService { get; private set; }
        #endregion

        #region Interface Methods
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        #endregion

        #region Private Methods
        private void ConfigureServices()
        {
            InitializeServiceProvider();
            InitializeServices();
            InitializeBusinessServices();
        }

        private void InitializeServiceProvider()
        {
            var services = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<ECERPDbContext>(options => options.UseInMemoryDatabase("Scratch"));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ECERPDbContext>();
            _serviceProvider = services.BuildServiceProvider();
        }

        private void InitializeServices()
        {
            _dbContext = _serviceProvider.GetRequiredService<ECERPDbContext>();
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _repository = new EntityFrameworkRepository<ECERPDbContext>(_dbContext);
        }

        private void InitializeBusinessServices()
        {
            ChartOfAccountsService = new ChartOfAccountsService(_repository);
            LedgerAccountService = new LedgerAccountService(_repository);
            CompanyService = new CompanyService(_repository, LedgerAccountService);
        }

        private void PopulateMockData()
        {
            Task.Run(PopulateUsers).Wait();
            PopulateCompanies();
            _dbContext.SaveChanges();
        }

        private async Task PopulateUsers()
        {
            var user_Admin = new ApplicationUser
            {
                UserName = "Admin"
            };

            await _userManager.CreateAsync(user_Admin, "Pass4Admin");
            await _repository.SaveAsync();

            Admin = user_Admin;
        }

        private void PopulateCompanies()
        {
            var companies = new List<Company>
            {
                new Company
                {
                    Name = "Putra Jaya",
                    ChartOfAccounts = new ChartOfAccounts()
                },
                new Company
                {
                    Name = "Puja Arta",
                    ChartOfAccounts = new ChartOfAccounts()
                },
                new Company
                {
                    Name = "Puja Mandiri",
                    ChartOfAccounts = new ChartOfAccounts()
                }
            };
            _dbContext.Companies.AddRange(companies);
        }
        #endregion
    }
}