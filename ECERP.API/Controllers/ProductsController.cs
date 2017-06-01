namespace ECERP.API.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Core;
    using Core.Domain;
    using Core.Domain.FinancialAccounting;
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
        #endregion

        #region Constructor
        public ProductsController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IProductService productService) : base(dbContext, signInManager, userManager)
        {
            _productService = productService;
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
            [FromQuery] string sortOrder,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            pageSize = pageSize == 0 ? int.MaxValue : pageSize;
            var filter = GenerateFilter(productIdFilter, nameFilter);
            var orderBy = GenerateSortOrder(sortOrder);
            var ledgerAccounts = _productService.GetProducts(filter, orderBy, pageIndex, pageSize);
            return
                new JsonResult(
                    Mapper.Map<IPagedList<Product>, PagedListViewModel<ProductViewModel>>(ledgerAccounts),
                    DefaultJsonSettings);
        }
        #endregion

        #region Utilities
        private static Expression<Func<Product, bool>> GenerateFilter(
            string productIdFilter,
            string nameFilter)
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

            return filter;
        }

        private static Func<IQueryable<Product>, IOrderedQueryable<Product>> GenerateSortOrder(
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "accountnumber_asc":
                    return x => x.OrderBy(la => la.ProductId);
                case "accountnumber_desc":
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