namespace ECERP.Services.Products
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Products;
    using Data.Abstract;

    public class ProductService : IProductService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public ProductService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets products
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Ledger Accounts</returns>
        public virtual IPagedList<Product> GetProducts(
            Expression<Func<Product, bool>> filter = null,
            Func<IQueryable<Product>, IOrderedQueryable<Product>> sortOrder = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            var skip = pageIndex * pageSize;
            var pagedProducts = _repository.Get(filter, sortOrder, skip, pageSize, p => p.ProductCategory);
            var totalCount = _repository.GetCount(filter);
            return new PagedList<Product>(pagedProducts, pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// Get product by identifier
        /// </summary>
        /// <param name="id">Product Identifier</param>
        /// <returns></returns>
        public virtual Product GetProductById(int id)
        {
            return _repository.GetById<Product>(id);
        }

        /// <summary>
        /// Insert a product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void InsertProduct(Product product)
        {
            _repository.Create(product);
            _repository.Save();
        }
        #endregion
    }
}