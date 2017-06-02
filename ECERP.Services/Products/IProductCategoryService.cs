namespace ECERP.Services.Products
{
    using System.Collections.Generic;
    using Core.Domain.Products;

    public interface IProductCategoryService
    {
        /// <summary>
        /// Gets all product categories
        /// </summary>
        /// <returns>A list of all product categories</returns>
        IList<ProductCategory> GetAllProductCategories();

        /// <summary>
        /// Get product category by identifier
        /// </summary>
        /// <param name="id">Product Category Identifier</param>
        /// <returns>Product Category</returns>
        ProductCategory GetProductCategoryById(int id);

        /// <summary>
        /// Get product category by name
        /// </summary>
        /// <param name="name">Product Category Name</param>
        /// <returns>Product Category</returns>
        ProductCategory GetProductCategoryByName(string name);

        /// <summary>
        /// Insert a product category
        /// </summary>
        /// <param name="productCategory">Product Category</param>
        void InsertProductCategory(ProductCategory productCategory);
    }
}