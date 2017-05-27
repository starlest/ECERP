namespace ECERP.Services.CompanySuppliers
{
    using System.Collections.Generic;
    using Core.Domain.Companies;
    using Core.Domain.Suppliers;

    public interface ICompanySupplierService
    {
        /// <summary>
        /// Gets a companySupplier by relating identifiers
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <param name="supplierId">Supplier Identifier</param>
        CompanySupplier GetCompanySupplier(int companyId, int supplierId);

        /// <summary>
        /// Gets a company's suppliers
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <returns>A list of company suppliers</returns>
        IList<Supplier> GetCompanySuppliers(int companyId);

        /// <summary>
        /// Gets a supplier's companies
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <returns>A list of supplier's companies</returns>
        IList<Company> GetSupplierCompanies(int supplierId);

        /// <summary>
        /// Registers a supplier with a company
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <param name="supplierId">Supplier Identifier</param>
        void Register(int companyId, int supplierId);

        /// <summary>
        /// Deregisters a supplier with a company
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <param name="supplierId">Supplier Identifier</param>
        void Deregister(int companyId, int supplierId);
    }
}
