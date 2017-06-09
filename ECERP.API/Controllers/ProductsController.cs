namespace ECERP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using Core;
    using Core.Domain;
    using Core.Domain.Products;
    using Core.Domain.Suppliers;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Products;
    using Services.Suppliers;
    using ViewModels;

    public class ProductsController : BaseController
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly ISupplierService _supplierService;
        private readonly ISupplierProductService _supplierProductService;
        #endregion

        #region Constructor
        public ProductsController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IProductService productService,
            IProductCategoryService productCategoryService,
            ISupplierService supplierService,
            ISupplierProductService supplierProductService) : base(dbContext, signInManager, userManager)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _supplierService = supplierService;
            _supplierProductService = supplierProductService;
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
            var products = _productService.GetProducts(filter, orderBy, pageIndex, pageSize);
            var productsVM = Mapper.Map<IPagedList<Product>, PagedListViewModel<ProductViewModel>>(products);
            foreach (var productVM in productsVM.Source)
            {
                productVM.Suppliers = GetProductSupplierNames(productVM.Id);
            }
            return new JsonResult(productsVM,DefaultJsonSettings);
        }

        /// <summary>
        ///     GET: products/{id}
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns>A Json-serialized object representing a single product.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound(new { Error = "Product not found." });
            var productVM = Mapper.Map<Product, ProductViewModel>(product);
            productVM.Suppliers = GetProductSupplierNames(productVM.Id);
            return new JsonResult(productVM, DefaultJsonSettings);
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
                    return NotFound(new { Error = "Product category not found." });

                var product = new Product
                {
                    ProductId = pvm.ProductId,
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

                var productVM = Mapper.Map<Product, ProductViewModel>(product);
                productVM.Suppliers = GetProductSupplierNames(productVM.Id);

                // return the newly-created ledger account to the client.
                return new JsonResult(productVM, DefaultJsonSettings);
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "Check that all the fields are valid." });
            }
        }

        /// <summary>
        /// PUT: products/{id}
        /// </summary>
        /// <returns>Updates an existing product and return it accordingly.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductViewModel pvm)
        {
            if (pvm == null) return NotFound(new { Error = "Product could not be found" });

            var product = _productService.GetProductById(id);

            if (product == null) return NotFound(new { Error = "Product could not be found" });

            var productCategory = _productCategoryService.GetProductCategoryByName(pvm.ProductCategory);

            if (productCategory == null)
                return NotFound(new { Error = "Product category could not be found." });

            // handle the update (on per-property basis)
            product.ProductId = pvm.ProductId;
            product.Name = pvm.Name;
            product.PrimaryUnitName = pvm.PrimaryUnitName;
            product.SecondaryUnitName = pvm.SecondaryUnitName;
            product.SalesPrice = pvm.SalesPrice / pvm.QuantityPerPrimaryUnit;
            product.PurchasePrice = pvm.PurchasePrice / pvm.QuantityPerPrimaryUnit;
            product.ProductCategoryId = productCategory.Id;

            _productService.UpdateProduct(product);

            product = _productService.GetProductById(id);

            var productVM = Mapper.Map<Product, ProductViewModel>(product);
            productVM.Suppliers = GetProductSupplierNames(productVM.Id);

            return new JsonResult(productVM, DefaultJsonSettings);
        }

        /// <summary>
        /// POST: suppliers/{id}/registersupplier
        /// </summary>
        /// <returns>Registers a product to a supplier and return it accordingly.</returns>
        [HttpPost("{id}/registersupplier")]
        public IActionResult RegisterSupplier(int id, [FromQuery] int supplierId)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound(new { Error = "Product could not be found" });

            var supplier = _supplierService.GetSupplierById(supplierId);
            if (supplier == null) return NotFound(new { Error = "Supplier could not be found" });

            var supplierProduct = _supplierProductService.GetSupplierProduct(supplierId, id);
            if (supplierProduct != null)
                return BadRequest(new { Error = "Product is already registered to supplier." });

            _supplierProductService.Register(supplierId, id);

            product = _productService.GetProductById(id);

            var productVM = Mapper.Map<Product, ProductViewModel>(product);
            productVM.Suppliers = GetProductSupplierNames(productVM.Id);

            return new JsonResult(productVM, DefaultJsonSettings);
        }

        /// <summary>
        /// POST: products/{id}/deregistersupplier
        /// </summary>
        /// <returns>Deregisters a product from a supplier and return it accordingly.</returns>
        [HttpPost("{id}/deregistersupplier")]
        public IActionResult DeregisterSupplier(int id, [FromQuery] int supplierId)
        {
            var supplierProduct = _supplierProductService.GetSupplierProduct(supplierId, id);
            if (supplierProduct == null) return BadRequest(new { Error = "Product is not registered to supplier." });

            _supplierProductService.Deregister(supplierId, id);

            var product = _productService.GetProductById(id);

            var productVM = Mapper.Map<Product, ProductViewModel>(product);
            productVM.Suppliers = GetProductSupplierNames(productVM.Id);

            return new JsonResult(productVM, DefaultJsonSettings);
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

        private IList<string> GetProductSupplierNames(int productId)
        {
            return _supplierProductService.GetProductSuppliers(productId).Select(c => c.Name).ToList();
        }
        #endregion
    }
}