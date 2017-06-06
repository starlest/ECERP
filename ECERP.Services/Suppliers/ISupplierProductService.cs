namespace ECERP.Services.Suppliers
{
    using System.Collections.Generic;
    using Core.Domain.Companies;
    using Core.Domain.Products;
    using Core.Domain.Suppliers;

    public interface ISupplierProductService
    {
        /// <summary>
        /// Gets a companySupplier by relating identifiers
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="productId">Product Identifier</param>
        SupplierProduct GetSupplierProduct(int supplierId, int productId);

        /// <summary>
        /// Gets a supplier's products
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <returns>A list of supplier products</returns>
        IList<Product> GetSupplierProducts(int supplierId);

        /// <summary>
        /// Gets a product's suppliers
        /// </summary>
        /// <param name="productId">Product Identifier</param>
        /// <returns>A list of product's suppliers</returns>
        IList<Supplier> GetProductSuppliers(int productId);

        /// <summary>
        /// Registers a product with a supplier
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="productId">Product Identifier</param>
        void Register(int supplierId, int productId);

        /// <summary>
        /// Deregisters a product with a supplier
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="productId">Product Identifier</param>
        void Deregister(int supplierId, int productId);
    }
}