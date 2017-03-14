namespace ECERP.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;
    using Data.Abstract;
    using Models.Entities;
    using Models.Entities.Companies;
    using Models.Entities.FinancialAccounting;

    public class CompanyService : ICompanyService
    {
        #region Private Members
        private readonly IRepository _repository;
        private readonly ILedgerAccountService _ledgerAccountService;
        #endregion

        #region Constructor
        public CompanyService(IRepository repository, ILedgerAccountService accountService)
        {
            _repository = repository;
            _ledgerAccountService = accountService;
        }
        #endregion

        #region Interface Methods
        public virtual IEnumerable<Company> GetAll()
        {
            return _repository.GetAll<Company>();
        }

        public Company GetSingleById(object id)
        {
            return _repository.GetById<Company>(id);
        }

        public Company GetSingleByName(string name)
        {
            return _repository.GetOne<Company>(c => c.Name.Equals(name));
        }

        public void CreateCompany(string name, ApplicationUser createdBy)
        {
            var company = new Company
            {
                Name = name,
                ChartOfAccounts = new ChartOfAccounts()
            };
            _repository.Create(company, createdBy);
            company.ChartOfAccounts.LedgerAccounts =
                GetDefaultLedgerAccountsForDistributionBusiness(company.ChartOfAccounts.Id).ToList();
            _repository.Save();
        }
        #endregion

        #region Private Methods
        private IEnumerable<LedgerAccount> GetDefaultLedgerAccountsForDistributionBusiness(int chartOfAccountsId)
        {
            // Create 15 essential accounts for a distribution business
            var defaultLedgerAccounts = new List<LedgerAccount>
            {
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.CashAndBank),
                    Name = "Cash",
                    Description =
                        "Checking account balance, currency, coins, checks received from customers but not yet deposited.",
                    Type = LedgerAccountType.Asset,
                    Group = LedgerAccountGroup.CashAndBank,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.Inventory),
                    Name = "Inventory",
                    Description =
                        "Cost of inventory purchased but has not yet been sold.",
                    Type = LedgerAccountType.Asset,
                    Group = LedgerAccountGroup.Inventory,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.SalesRevenue),
                    Name = "Sales Revenue",
                    Description =
                        "Amounts earned from providing goods to clients, either for cash or on credit during the accounting period.",
                    Type = LedgerAccountType.Revenue,
                    Group = LedgerAccountGroup.SalesRevenue,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.CostOfGoodsSold),
                    Name = "Cost Of Goods Sold",
                    Description =
                        "The cost of the goods sold by the company to clients, either for cash or on credit during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.CostOfGoodsSold,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.SellingExpenses),
                    Name = "Comissions Expense",
                    Description = "The amount of commissions paid to salesmen during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.SellingExpenses) +
                        1,
                    Name = "Freight Expense",
                    Description = "Cost for delivering goods to customers during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.SellingExpenses) +
                        2,
                    Name = "Cost of Sales-Freight",
                    Description = "Cost of bringing in goods from suppliers.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.SellingExpenses) +
                        3,
                    Name = "Salaries Expense (Selling)",
                    Description = "The amount of salaries paid to sales employees during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.SellingExpenses) +
                        4,
                    Name = "Other Expense (Selling)",
                    Description = "Other selling expenses for the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId,
                            LedgerAccountGroup.AdministrativeExpenses),
                    Name = "Salaries Expense (Administrative)",
                    Description =
                        "The amount of salaries paid to administrative employees during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId,
                            LedgerAccountGroup.AdministrativeExpenses) + 1,
                    Name = "Office Supplies Expense",
                    Description = "The cost of supplies purchased for usage in the office during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId,
                            LedgerAccountGroup.AdministrativeExpenses) + 2,
                    Name = "Office Equipment Expense",
                    Description =
                        "The cost of equipment purchased for usage in the office during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId,
                            LedgerAccountGroup.AdministrativeExpenses) + 3,
                    Name = "Utilities Expense",
                    Description =
                        "Costs for electricity, heat, water, and sewer that were used during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId,
                            LedgerAccountGroup.AdministrativeExpenses) + 4,
                    Name = "Telephone Expense",
                    Description =
                        "Cost of telephone used during the current accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId,
                            LedgerAccountGroup.AdministrativeExpenses) + 5,
                    Name = "Other Expense (Administrative)",
                    Description =
                        "Other administrative expense for the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                }
            };
            return defaultLedgerAccounts;
        }
        #endregion
    }
}