namespace ECERP.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Core.Domain;
    using Core.Domain.Products;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Products;
    using ViewModels;

    public class ProductCategoriesController: BaseController
    {
        #region Fields
        private readonly IProductCategoryService _productCategoryService;
        #endregion

        #region Constructor
        public ProductCategoriesController(ECERPDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IProductCategoryService productCategoryService) : base(dbContext, signInManager, userManager)
        {
            _productCategoryService = productCategoryService;
        }
        #endregion

        #region RESTful Conventions
        /// <summary>
        /// GET: productcategories
        /// </summary>
        /// <returns>An array of all Json-serialized product categories.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var productCategories = _productCategoryService.GetAllProductCategories();
            return new JsonResult(Mapper.Map<IList<ProductCategory>, IList<ProductCategoryViewModel>>(productCategories), DefaultJsonSettings);
        }

        /// <summary>
        /// POST: productcategories
        /// </summary>
        /// <returns>Creates a new Product Category and return it accordingly.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] ProductCategoryViewModel pcvm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var productCategory = new ProductCategory()
                {
                    Name = pcvm.Name
                };
                _productCategoryService.InsertProductCategory(productCategory);
                return new JsonResult(Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory), DefaultJsonSettings);
            }
            catch (Exception)
            {
                // return the error.
                return BadRequest(new { Error = "Check that all the fields are valid." });
            }
        }
        #endregion
    }
}
