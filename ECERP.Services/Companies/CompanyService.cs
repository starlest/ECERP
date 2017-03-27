namespace ECERP.Services.Companies
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Companies;
    using Core.Domain.FinancialAccounting;
    using Data.Abstract;

    public class CompanyService : ICompanyService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public CompanyService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>Companies</returns>
        public virtual IList<Company> GetAllCompanies()
        {
            return _repository.GetAll<Company>().ToList();
        }

        /// <summary>
        /// Gets a company by identifier
        /// </summary>
        /// <param name="id">Company identifier</param>
        /// <returns>A company</returns>
        public virtual Company GetCompanyById(int id)
        {
            return _repository.GetById<Company>(id);
        }

        /// <summary>
        /// Gets a company by name
        /// </summary>
        /// <param name="name">Company name</param>
        /// <returns>A company</returns>
        public virtual Company GetCompanyByName(string name)
        {
            return _repository.GetOne<Company>(x => x.Name == name);
        }


        /// <summary>
        /// Insert a company
        /// </summary>
        /// <param name="company">Company</param>
        public virtual void InsertCompany(Company company)
        {
            company.ChartOfAccounts.LedgerAccounts = GetDefaultLedgerAccountsForDistributionBusiness();
            _repository.Create(company);
            _repository.Save();
        }
        #endregion

        #region Utilities
        private static IList<LedgerAccount> GetDefaultLedgerAccountsForDistributionBusiness()
        {
            // Create 16 essential accounts for a distribution business
            var defaultLedgerAccounts = new List<LedgerAccount>
            {
                new LedgerAccount
                {
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.CashAndBank),
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.Inventory),
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.SalesRevenue),
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.CostOfGoodsSold),
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.SellingExpenses),
                    Name = "Comissions Expense",
                    Description = "The amount of commissions paid to salesmen during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.SellingExpenses) + 1,
                    Name = "Freight Expense",
                    Description = "Cost for delivering goods to customers during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.SellingExpenses) + 2,
                    Name = "Cost of Sales-Freight",
                    Description = "Cost of bringing in goods from suppliers.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.SellingExpenses) + 3,
                    Name = "Salaries Expense (Selling)",
                    Description = "The amount of salaries paid to sales employees during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.SellingExpenses) + 4,
                    Name = "Other Expense (Selling)",
                    Description = "Other selling expenses for the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.AdministrativeExpenses),
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.AdministrativeExpenses) + 1,
                    Name = "Office Supplies Expense",
                    Description = "The cost of supplies purchased for usage in the office during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.AdministrativeExpenses) + 2,
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.AdministrativeExpenses) + 3,
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.AdministrativeExpenses) + 4,
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
                    AccountNumber = GetGroupAccountNumber(LedgerAccountGroup.AdministrativeExpenses) + 5,
                    Name = "Other Expense (Administrative)",
                    Description =
                        "Other administrative expense for the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true
                },
                new LedgerAccount
                {
                    AccountNumber =
                        GetGroupAccountNumber(LedgerAccountGroup.CommonStock),
                    Name = "Capital",
                    Description =
                        "Amount the owner invested in the company not withdrawn by the owner.",
                    Type = LedgerAccountType.Equity,
                    Group = LedgerAccountGroup.CommonStock,
                    IsActive = true,
                    IsDefault = true
                }
            };
            return defaultLedgerAccounts;
        }

        private static int GetGroupAccountNumber(LedgerAccountGroup group) => (int)group * 10000 + 1;
        #endregion
    }
}