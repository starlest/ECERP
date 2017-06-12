namespace ECERP.Services.Suppliers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Products;
    using Core.Domain.Suppliers;
    using Data.Abstract;

    public class SupplierProductService : ISupplierProductService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public SupplierProductService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
//        /// <summary>
//        /// Gets a supplierProduct by relating identifiers
//        /// </summary>
//        /// <param name="supplierId">Supplier Identifier</param>
//        /// <param name="productId">Product Identifier</param>
//        public virtual SupplierProduct GetSupplierProduct(int supplierId, int productId)
//        {
//            return
//                _repository.GetOne<SupplierProduct>(
//                    cs => cs.SupplierId.Equals(supplierId) && cs.ProductId.Equals(productId));
//        }
//
//        /// <summary>
//        /// Gets a supplier's products
//        /// </summary>
//        /// <param name="supplierId">Supplier Identifier</param>
//        /// <returns>A list of supplier products</returns>
//        public virtual IList<Product> GetSupplierProducts(int supplierId)
//        {
//            var supplierProducts = _repository.Get<SupplierProduct>(cs => cs.SupplierId.Equals(supplierId), null, null,
//                null, cs => cs.Product);
//            return supplierProducts.Select(cs => cs.Product).ToList();
//        }
//
//        /// <summary>
//        /// Gets a product's suppliers
//        /// </summary>
//        /// <param name="productId">Product Identifier</param>
//        /// <returns>A list of product's suppliers</returns>
//        public virtual IList<Supplier> GetProductSuppliers(int productId)
//        {
//            var productSuppliers = _repository.Get<SupplierProduct>(sp => sp.ProductId.Equals(productId), null, null,
//                null, sp => sp.Supplier);
//            return productSuppliers.Select(sp => sp.Supplier).OrderBy(c => c.Name).ToList();
//        }
//
//        /// <summary>
//        /// Registers a product with a supplier
//        /// </summary>
//        /// <param name="supplierId">Supplier Identifier</param>
//        /// <param name="productId">Product Identifier</param>
//        public virtual void Register(int supplierId, int productId)
//        {
//            var supplier = _repository.GetById<Supplier>(supplierId);
//            if (supplier == null)
//                throw new ArgumentException("Supplier does not exist.");
//
//            var product = _repository.GetById<Product>(productId);
//            if (product == null)
//                throw new ArgumentException("Product does not exist.");
//
//            var supplerProduct = new SupplierProduct()
//            {
//                SupplierId = supplierId,
//                ProductId = productId
//            };
//
//            _repository.Create(supplerProduct);
//            _repository.Save();
//        }
//
//        /// <summary>
//        /// Deregisters a product with a supplier
//        /// </summary>
//        /// <param name="supplierId">Supplier Identifier</param>
//        /// <param name="productId">Product Identifier</param>
//        public virtual void Deregister(int supplierId, int productId)
//        {
//            var supplierProduct = GetSupplierProduct(supplierId, productId);
//            if (supplierProduct == null)
//                throw new ArgumentException("SupplierProduct does not exist.");
//            _repository.Delete<SupplierProduct>(supplierProduct.Id);
//            _repository.Save();
//        }
        #endregion
    }
}