﻿namespace ECERP.Services.ProductCategories
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Products;
    using Data.Abstract;

    public class ProductCategoryService : IProductCategoryService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public ProductCategoryService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets all product categories
        /// </summary>
        /// <returns>A list of all product categories</returns>
        public virtual IList<ProductCategory> GetAllProductCategories()
        {
            return _repository.GetAll<ProductCategory>(x => x.OrderBy(pc => pc.Name)).ToList();
        }

        /// <summary>
        /// Get product category by identifier
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <returns></returns>
        public virtual ProductCategory GetProductCategoryById(int id)
        {
            return _repository.GetById<ProductCategory>(id);
        }

        /// <summary>
        /// Insert a product category
        /// </summary>
        /// <param name="productCategory">Product Category</param>
        public virtual void InsertProductCategory(ProductCategory productCategory)
        {
            _repository.Create(productCategory);
            _repository.Save();
        }
        #endregion
    }
}