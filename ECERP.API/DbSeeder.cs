﻿namespace ECERP.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Domain;
    using Core.Domain.Cities;
    using Core.Domain.Companies;
    using Core.Domain.Products;
    using Core.Domain.Suppliers;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using OpenIddict.Core;
    using OpenIddict.Models;
    using Services.Cities;
    using Services.Companies;
    using Services.Products;
    using Services.Suppliers;

    public class DbSeeder
    {
        #region Private Members
        private readonly ECERPDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISupplierService _suppliersService;
        private readonly ICityService _citiesService;
        private readonly ICompanyService _companyService;
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public DbSeeder(ECERPDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ISupplierService suppliersService,
            ICityService citiesService,
            ICompanyService companyService,
            IProductService productService,
            IProductCategoryService productCategoryService,
            OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _suppliersService = suppliersService;
            _citiesService = citiesService;
            _companyService = companyService;
            _productService = productService;
            _productCategoryService = productCategoryService;
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

            // Create default cities
            if (!_dbContext.Cities.Any()) CreateCities();

            // Create default Suppliers
            if (!_dbContext.Suppliers.Any()) CreateSuppliers();

            // Create default Product Categories
            if (!_dbContext.ProductCategories.Any()) CreateProductCategories();

            // Create default Products
            if (!_dbContext.Products.Any()) CreateProducts();
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

        private void CreateCities()
        {
            var cities = new List<City>
            {
                new City
                {
                    Name = "Palembang"
                },
                new City
                {
                    Name = "Jakarta"
                },
                new City
                {
                    Name = "Tembilahan"
                }
            };

            foreach (var city in cities)
            {
                _citiesService.InsertCity(city);
            }
        }

        private void CreateSuppliers()
        {
            var city_Palembang = _citiesService.GetCityByName("Palembang");
            var city_Jakarta = _citiesService.GetCityByName("Jakarta");

            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Name = "Interbis",
                    Address = "Palembang",
                    CityId = city_Palembang.Id,
                    ContactNumber = "00001",
                    TaxId = "002324"
                },
                new Supplier
                {
                    Name = "Arta Boga",
                    Address = "Palembang",
                    CityId = city_Palembang.Id,
                    ContactNumber = "00002",
                    TaxId = "002324"
                },
                new Supplier
                {
                    Name = "ABC",
                    Address = "Jakarta",
                    CityId = city_Jakarta.Id,
                    ContactNumber = "00003",
                    TaxId = "002324"
                }
            };

            foreach (var supplier in suppliers)
            {
                _suppliersService.InsertSupplier(supplier);
            }
        }

        private void CreateProductCategories()
        {
            var productCategories = new List<ProductCategory>
            {
                new ProductCategory
                {
                    Name = "Beverages"
                },
                new ProductCategory
                {
                    Name = "Sauces"
                }
            };

            foreach (var productCategory in productCategories)
            {
                _productCategoryService.InsertProductCategory(productCategory);
            }
        }

        private void CreateProducts()
        {
            var productCategory_Sauces = _productCategoryService.GetProductCategoryByName("Sauces");
            var productCategory_Beverages = _productCategoryService.GetProductCategoryByName("Beverages");

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Sambal ABC",
                    ProductId = "ABC001",
                    PrimaryUnitName = "BOX",
                    SecondaryUnitName = "",
                    QuantityPerPrimaryUnit = 12,
                    QuantityPerSecondaryUnit = 0,
                    SalesPrice = 5000,
                    PurchasePrice = 4000,
                    ProductCategoryId = productCategory_Sauces.Id
                },
                new Product
                {
                    Name = "Teh Gelas 48",
                    ProductId = "ARTA001",
                    PrimaryUnitName = "BOX",
                    SecondaryUnitName = "",
                    QuantityPerPrimaryUnit = 48,
                    QuantityPerSecondaryUnit = 0,
                    SalesPrice = 6000,
                    PurchasePrice = 4000,
                    ProductCategoryId = productCategory_Beverages.Id
                }
            };

            foreach (var product in products)
            {
                _productService.InsertProduct(product);
            }
        }
    }
    #endregion
}