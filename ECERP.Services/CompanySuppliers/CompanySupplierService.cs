namespace ECERP.Services.CompanySuppliers
{
    using System;
    using Core.Domain.Companies;
    using Core.Domain.Suppliers;
    using Data.Abstract;
    using System.Collections.Generic;
    using System.Linq;

    public class CompanySupplierService : ICompanySupplierService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public CompanySupplierService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a companySupplier by relating identifiers
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <param name="supplierId">Supplier Identifier</param>
        public virtual CompanySupplier GetCompanySupplier(int companyId, int supplierId)
        {
            return
                _repository.GetOne<CompanySupplier>(
                    cs => cs.CompanyId.Equals(companyId) && cs.SupplierId.Equals(supplierId));
        }

        /// <summary>
        /// Gets a company's suppliers
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <returns>A list of company's suppliers</returns>
        public virtual IList<Supplier> GetCompanySuppliers(int companyId)
        {
            var companySuppliers = _repository.Get<CompanySupplier>(cs => cs.CompanyId.Equals(companyId), null, null, null, cs => cs.Supplier);
            return companySuppliers.Select(cs => cs.Supplier).ToList();
        }

        /// <summary>
        /// Gets a supplier's companies
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <returns>A list of supplier's companies</returns>
        public virtual IList<Company> GetSupplierCompanies(int supplierId)
        {
            var companySuppliers = _repository.Get<CompanySupplier>(cs => cs.SupplierId.Equals(supplierId), null, null,
                null, cs => cs.Company);
            return companySuppliers.Select(cs => cs.Company).OrderBy(c => c.Name).ToList();
        }

        /// <summary>
        /// Registers a supplier with a company
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <param name="supplierId">Supplier Identifier</param>
        public virtual void Register(int companyId, int supplierId)
        {
            var company = _repository.GetById<Company>(companyId);
            if (company == null)
                throw new ArgumentException("Company does not exist.");

            var supplier = _repository.GetById<Supplier>(supplierId);
            if (supplier == null)
                throw new ArgumentException("Supplier does not exist.");

            var companySupplier = new CompanySupplier
            {
                CompanyId = companyId,
                SupplierId = supplierId
            };

            _repository.Create(companySupplier);
            _repository.Save();
        }

        /// <summary>
        /// Deregisters a supplier with a company
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <param name="supplierId">Supplier Identifier</param>
        public void Deregister(int companyId, int supplierId)
        {
            var companySupplier = GetCompanySupplier(companyId, supplierId);
            if (companySupplier == null)
                throw new ArgumentException("CompanySupplier does not exist.");
            _repository.Delete<CompanySupplier>(companySupplier.Id);
            _repository.Save();
        }
        #endregion
    }
}