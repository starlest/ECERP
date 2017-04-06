namespace ECERP.API
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Domain;
    using Core.Domain.Companies;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using OpenIddict.Core;
    using OpenIddict.Models;
    using Services.Companies;

    public class DbSeeder
    {
        #region Private Members
        private readonly ECERPDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public DbSeeder(ECERPDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ICompanyService companyService,
            OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _companyService = companyService;
            _applicationManager = applicationManager;
            _configuration = configuration;
        }
        #endregion

        #region Public Methods
        public async Task SeedAsync()
        {
            // Create the Db if it doesn't exist
            _dbContext.Database.EnsureCreated();
            // Create default Application
            if (await _applicationManager.FindByIdAsync("ECERP", CancellationToken.None) == null)
                await CreateApplication();
            // Create default Users
            if (await _dbContext.Users.CountAsync() == 0) await CreateUsersAsync();

#if DEBUG
            // Create default Companies
            if (!_dbContext.Companies.Any()) CreateCompanies();
#endif
        }
        #endregion

        #region Seed Methods
        private async Task CreateApplication()
        {
            await _applicationManager.CreateAsync(new OpenIddictApplication
            {
                Id = _configuration["Authentication:OpenIddict:ApplicationId"],
                DisplayName = _configuration["Authentication:OpenIddict:DisplayName"],
                RedirectUri = _configuration["Authentication:OpenIddict:RedirectUri"],
                LogoutRedirectUri = _configuration["Authentication:OpenIddict:LogoutRedirectUri"],
                ClientId = _configuration["Authentication:OpenIddict:ClientId"],
                Type = OpenIddictConstants.ClientTypes.Public
            }, CancellationToken.None);
        }

        private async Task CreateUsersAsync()
        {
            // local variables 
            var createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            const string role_Administrator = "Administrator";

            // Create Roles (if they don't exist yet)
            if (!await _roleManager.RoleExistsAsync(role_Administrator))
                await _roleManager.CreateAsync(new IdentityRole(role_Administrator));

            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var user_Admin = new ApplicationUser()
            {
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Admin",
                CreatedDate = createdDate,
                ModifiedDate = createdDate,
                Email = "support@edemy.sg",
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            // Insert "Admin" into the Database and also assign the "Administrator" role to him.
            if (await _userManager.FindByIdAsync(user_Admin.Id) == null)
            {
                await _userManager.CreateAsync(user_Admin, "Pass4Admin");
                await _userManager.AddToRoleAsync(user_Admin, role_Administrator);
            }

            await _dbContext.SaveChangesAsync();
        }

        private void CreateCompanies()
        {
            _companyService.InsertCompany(new Company { Name = "Putra Jaya" });
        }
        #endregion
    }
}