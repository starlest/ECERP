namespace ECERP.Services.SupplierSubscriptions
{
    using System.Collections.Generic;
    using Core.Domain.Companies;
    using Core.Domain.Suppliers;

    public interface ISupplierSubscriptionService
    {
        /// <summary>
        /// Gets a supplier subscription by relating identifiers
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="companyId">Company Identifier</param>
        SupplierSubscription GetSupplierSubscription(int supplierId, int companyId);

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
        /// Subscribe supplier with a company
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="companyId">Company Identifier</param>
        void Subscribe(int supplierId, int companyId);

        /// <summary>
        /// Unsubscribe a supplier from a company
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="companyId">Company Identifier</param>
        void Unsubscribe(int supplierId, int companyId);
    }
}
