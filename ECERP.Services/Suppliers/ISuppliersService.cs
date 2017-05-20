namespace ECERP.Services.Suppliers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Suppliers;

    public interface ISuppliersService
    {
        /// <summary>
        /// Gets suppliers
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Suppliers</returns>
        IPagedList<Supplier> GetSuppliers(
            Expression<Func<Supplier, bool>> filter = null,
            Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>> sortOrder = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a supplier
        /// </summary>
        /// <param name="id">Supplier Identifier</param>
        /// <returns>Supplier</returns>
        Supplier GetSupplierById(int id);

        /// <summary>
        /// Gets a supplier
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Supplier</returns>
        Supplier GetSupplierByName(string name);

        /// <summary>
        /// Inserts a supplier
        /// </summary>
        /// <param name="supplier">Supplier</param>
        void InsertSupplier(Supplier supplier);

        /// <summary>
        /// Updates a supplier
        /// </summary>
        /// <param name="supplier">Supplier</param>
        void UpdateSupplier(Supplier supplier);

        /// <summary>
        /// Registers a supplier with the company
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="companyId">Company Identifier</param>
        void RegisterSupplierToCompany(int supplierId, int companyId);
    }
}
