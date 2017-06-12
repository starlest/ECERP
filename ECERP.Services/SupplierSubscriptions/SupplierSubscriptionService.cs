namespace ECERP.Services.SupplierSubscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Companies;
    using Core.Domain.FinancialAccounting;
    using Core.Domain.Suppliers;
    using Data.Abstract;
    using FinancialAccounting;

    public class SupplierSubscriptionService : ISupplierSubscriptionService
    {
        #region Fields
        private readonly IRepository _repository;
        private readonly ILedgerAccountService _ledgerAccountService;
        #endregion

        #region Constructor
        public SupplierSubscriptionService(IRepository repository,
            ILedgerAccountService ledgerAccountService)
        {
            _repository = repository;
            _ledgerAccountService = ledgerAccountService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a supplier subscription by relating identifiers
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="companyId">Company Identifier</param>
        public virtual SupplierSubscription GetSupplierSubscription(int supplierId, int companyId)
        {
            return
                _repository.GetOne<SupplierSubscription>(
                    cs => cs.CompanyId.Equals(companyId) && cs.SupplierId.Equals(supplierId));
        }

        /// <summary>
        /// Gets a company's suppliers
        /// </summary>
        /// <param name="companyId">Company Identifier</param>
        /// <returns>A list of company's suppliers</returns>
        public virtual IList<Supplier> GetCompanySuppliers(int companyId)
        {
            var supplierSubscriptions = _repository.Get<SupplierSubscription>(
                cs => cs.CompanyId.Equals(companyId),
                null,
                null,
                null, cs => cs.Supplier);
            return supplierSubscriptions.Select(cs => cs.Supplier).ToList();
        }

        /// <summary>
        /// Gets a supplier's companies
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <returns>A list of supplier's companies</returns>
        public virtual IList<Company> GetSupplierCompanies(int supplierId)
        {
            var companySuppliers =
                _repository.Get<SupplierSubscription>(
                    cs => cs.SupplierId.Equals(supplierId) && cs.IsActive,
                    null, 
                    null,
                    null,
                    cs => cs.Company);
            return companySuppliers.Select(cs => cs.Company).OrderBy(c => c.Name).ToList();
        }

        /// <summary>
        /// Subscribe supplier with a company
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="companyId">Company Identifier</param>
        public virtual void Subscribe(int supplierId, int companyId)
        {
            // if relationship already exists, activate it and exit
            var supplierSubscription = GetSupplierSubscription(supplierId, companyId);

            if (supplierSubscription != null)
            {
                if (supplierSubscription.IsActive)
                    throw new ArgumentException("Subscription already exists.");
                supplierSubscription.IsActive = true;
                _repository.Save();
                return;
            }

            var company = _repository.GetById<Company>(companyId, c => c.ChartOfAccounts);
            if (company == null)
                throw new ArgumentException("Company does not exist.");

            var supplier = _repository.GetById<Supplier>(supplierId);
            if (supplier == null)
                throw new ArgumentException("Supplier does not exist.");

            var ledgerAccount = new LedgerAccount
            {
                AccountNumber = _ledgerAccountService.GenerateNewAccountNumber(LedgerAccountGroup.AccountsPayable),
                Name = $"{company.Name} - {supplier.Name} Accounts Payable",
                Description = $"Amounts owed to {supplier.Name}",
                ChartOfAccountsId = company.ChartOfAccounts.Id,
                IsHidden = true,
                IsDefault = true,
                Type = LedgerAccountType.Liability,
                Group = LedgerAccountGroup.AccountsPayable
            };

            supplierSubscription = new SupplierSubscription
            {
                CompanyId = companyId,
                SupplierId = supplierId,
                LedgerAccount = ledgerAccount
            };

            _repository.Create(supplierSubscription);
            _repository.Save();
        }

        /// <summary>
        /// Unsubscribe a supplier from a company
        /// </summary>
        /// <param name="supplierId">Supplier Identifier</param>
        /// <param name="companyId">Company Identifier</param>
        public void Unsubscribe(int supplierId, int companyId)
        {
            var supplierSubscription = GetSupplierSubscription(supplierId, companyId);
            if (supplierSubscription == null)
                throw new ArgumentException("Supplier subscription does not exist.");
            supplierSubscription.IsActive = false;
            _repository.Save();
        }
        #endregion
    }
}