namespace ECERP.Services.Products
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Products;

    public interface IProductService
    {
        /// <summary>
        /// Gets products
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Ledger Accounts</returns>
        IPagedList<Product> GetProducts(
            Expression<Func<Product, bool>> filter = null,
            Func<IQueryable<Product>, IOrderedQueryable<Product>> sortOrder = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Get product by identifier
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <returns></returns>
        Product GetProductById(int id);

        /// <summary>
        /// Insert a product
        /// </summary>
        /// <param name="product">Product</param>
        void InsertProduct(Product product);

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateProduct(Product product);
    }
}