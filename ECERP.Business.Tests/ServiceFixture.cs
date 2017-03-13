namespace ECERP.Business.Tests
{
    using System;
    using System.Collections.Generic;
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
        private ICompanyRepository _companyRepository;
        private IChartOfAccountsRepository _chartOfAccountsRepository;
        private ILedgerAccountRepository _ledgerAccountRepository;
        #endregion

        #region Constructor
        public ServiceFixture()
        {
            ConfigureServices();
            PopulateMockData();
        }
        #endregion

        #region Interface Properties
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
            InitializeRepositories();
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
        }

        private void InitializeRepositories()
        {
            _companyRepository = new CompanyRepository(_dbContext);
            _chartOfAccountsRepository = new ChartOfAccountsRepository(_dbContext);
            _ledgerAccountRepository = new LedgerAccountRepository(_dbContext);
        }

        private void InitializeBusinessServices()
        {
            ChartOfAccountsService = new ChartOfAccountsService(_chartOfAccountsRepository);
            LedgerAccountService = new LedgerAccountService(_ledgerAccountRepository);
            CompanyService = new CompanyService(_companyRepository, LedgerAccountService);
        }

        private void PopulateMockData()
        {
            PopulateCompanies();
            _dbContext.SaveChanges();
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