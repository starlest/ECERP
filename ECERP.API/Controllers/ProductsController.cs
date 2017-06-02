namespace ECERP.API.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Core;
    using Core.Domain;
    using Core.Domain.Products;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Products;
    using ViewModels;

    public class ProductsController : BaseController
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        #endregion

        #region Constructor
        public ProductsController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IProductService productService,
            IProductCategoryService productCategoryService) : base(dbContext, signInManager, userManager)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: products
        /// </summary>
        /// <returns>An array of all Json-serialized products.</returns>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] string productIdFilter,
            [FromQuery] string nameFilter,
            [FromQuery] string productCategoryFilter,
            [FromQuery] string isActiveFilter,
            [FromQuery] string sortOrder,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            pageSize = pageSize == 0 ? int.MaxValue : pageSize;
            var filter = GenerateFilter(productIdFilter, nameFilter, productCategoryFilter, isActiveFilter);
            var orderBy = GenerateSortOrder(sortOrder);
            var ledgerAccounts = _productService.GetProducts(filter, orderBy, pageIndex, pageSize);
            return
                new JsonResult(
                    Mapper.Map<IPagedList<Product>, PagedListViewModel<ProductViewModel>>(ledgerAccounts),
                    DefaultJsonSettings);
        }

        /// <summary>
        /// POST: products
        /// </summary>
        /// <returns>Creates a new Product and return it accordingly.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] ProductViewModel pvm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // TODO: get the user creating the ledger account
                // get the admin creating the student
                //                var adminId = GetCurrentUserId();
                //                if (adminId == null) return NotFound(new { error = "User is not authenticated." });
                //                var admin = _dbContext.Admins.SingleOrDefault(i => i.Id == adminId);
                //                if (admin == null) return NotFound(new { error = $"User ID {adminId} has not been found" });

                var productCategory = _productCategoryService.GetProductCategoryByName(pvm.ProductCategory);

                if (productCategory == null) 
                    return NotFound("Product category does not exists.");

                var product = new Product
                {
                    ProductId = pvm.Name,
                    Name = pvm.Name,
                    PrimaryUnitName = pvm.PrimaryUnitName,
                    SecondaryUnitName = pvm.SecondaryUnitName,
                    QuantityPerPrimaryUnit = pvm.QuantityPerPrimaryUnit,
                    QuantityPerSecondaryUnit = pvm.QuantityPerSecondaryUnit,
                    SalesPrice = pvm.SalesPrice / pvm.QuantityPerPrimaryUnit,
                    PurchasePrice = pvm.PurchasePrice / pvm.QuantityPerPrimaryUnit,
                    ProductCategoryId = productCategory.Id
                };

                _productService.InsertProduct(product);

                // return the newly-created ledger account to the client.
                return new JsonResult(Mapper.Map<Product, ProductViewModel>(product),
                    DefaultJsonSettings);
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "Check that all the fields are valid." });
            }
        }
        #endregion

        #region Utilities
        private static Expression<Func<Product, bool>> GenerateFilter(
            string productIdFilter,
            string nameFilter,
            string productCategoryFilter,
            string isActiveFilter)
        {
            var filter = PredicateBuilder.True<Product>();

            if (!string.IsNullOrEmpty(productIdFilter))
                filter =
                    filter.And(
                        x =>
                            x.ProductId.ToString()
                                .IndexOf(productIdFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(nameFilter))
                filter = filter.And(x => x.Name.IndexOf(nameFilter, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (!string.IsNullOrEmpty(productCategoryFilter))
                filter =
                    filter.And(
                        x =>
                            x.ProductCategory.Name.IndexOf(productCategoryFilter, 0,
                                StringComparison.CurrentCultureIgnoreCase) !=
                            -1);

            if (!string.IsNullOrEmpty(isActiveFilter))
                filter =
                    filter.And(
                        x =>
                            x.IsActive.ToString().IndexOf(isActiveFilter, 0, StringComparison.CurrentCultureIgnoreCase) !=
                            -1);
            return filter;
        }

        private static Func<IQueryable<Product>, IOrderedQueryable<Product>> GenerateSortOrder(
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "productid_asc":
                    return x => x.OrderBy(la => la.ProductId);
                case "productid_desc":
                    return x => x.OrderByDescending(la => la.ProductId);
                case "name_asc":
                    return x => x.OrderBy(la => la.Name);
                case "name_desc":
                    return x => x.OrderByDescending(la => la.Name);
                default:
                    return null;
            }
        }
        #endregion
    }
}