namespace ECERP.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Abstract;
    using Abstract.FinancialAccounting;
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
        public CompanyService(IRepository repository, ILedgerAccountService ledgerAccountService)
        {
            _repository = repository;
            _ledgerAccountService = ledgerAccountService;
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

        public void Create(string name, string createdBy)
        {
            var company = new Company
            {
                Name = name,
                ChartOfAccounts = new ChartOfAccounts()
            };
            _repository.Create(company, createdBy);
            company.ChartOfAccounts.LedgerAccounts =
                GetDefaultLedgerAccountsForDistributionBusiness(company.ChartOfAccounts.Id).ToList();
            var systemParameter = new SystemParameter
            {
                Key = Constants.LedgerCurrentPeriodStartDate,
                Value = DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1).ToString(CultureInfo.InvariantCulture),
                CompanyId = company.Id
            };
            _repository.Create(systemParameter, createdBy);
            _repository.Save();
        }

        public void OpenLastLedgerPeriod(int companyId, string modifiedBy)
        {
            var currentPeriodStartDateParamater = _repository.GetOne<SystemParameter>(
                p => p.CompanyId.Equals(companyId) && p.Key.Equals(Constants.LedgerCurrentPeriodStartDate));
            var currentPeriodStartDate = DateTime.Parse(currentPeriodStartDateParamater.Value);
            // Shift current period back by a month
            currentPeriodStartDateParamater.Value =
                currentPeriodStartDate.AddMonths(-1).ToString(CultureInfo.InvariantCulture);
            _repository.Update(currentPeriodStartDateParamater, modifiedBy);
        }

        // TODO: Test Case
        public void CloseCurrentLedgerPeriod(int companyId, string modifiedBy)
        {
            var currentPeriodStartDateParamater = _repository.GetOne<SystemParameter>(
                p => p.CompanyId.Equals(companyId) && p.Key.Equals(Constants.LedgerCurrentPeriodStartDate));
            var currentPeriodStartDate = DateTime.Parse(currentPeriodStartDateParamater.Value);

            ClosePeriod(companyId, currentPeriodStartDate.Year, currentPeriodStartDate.Month, modifiedBy);

            // Shift current period forward by a month
            currentPeriodStartDateParamater.Value =
                currentPeriodStartDate.AddMonths(1).ToString(CultureInfo.InvariantCulture);
            _repository.Update(currentPeriodStartDateParamater, modifiedBy);

            _repository.Save();
        }

        private void ClosePeriod(int companyId, int year, int month, string modifiedBy)
        {
            if (month < 1 || month > 12)
                throw new Exception("Invalid closing period");

            var coa = _repository.GetOne<ChartOfAccounts>(c => c.CompanyId.Equals(companyId), c => c.LedgerAccounts);
            var closingPeriodLedgerAccounts = coa.LedgerAccounts.Where(
                account => account.Type.Equals(LedgerAccountType.Asset) &&
                           account.Type.Equals(LedgerAccountType.Liability) &&
                           account.Type.Equals(LedgerAccountType.Equity) &&
                           account.Type.Equals(LedgerAccountType.ContraAsset) &&
                           account.Type.Equals(LedgerAccountType.ContraLiability) &&
                           account.Type.Equals(LedgerAccountType.ContraEquity));

            foreach (var account in closingPeriodLedgerAccounts)
            {
                var accountPeriodLedgerTransactionLines = _repository.Get<LedgerTransactionLine>(
                        line =>
                            line.LedgerAccountId.Equals(account.Id) &&
                            (line.Transaction as LedgerTransaction).PostingDate.Year.Equals(year) &&
                            (line.Transaction as LedgerTransaction).PostingDate.Month.Equals(month))
                    .ToList();

                // Get the starting balance of the account
                var accountPeriodStartingBalance = month == 1
                    ? _ledgerAccountService.GetPeriodBalance(account, year, 0)
                    : _ledgerAccountService.GetPeriodBalance(account, year, month);
                var accountPeriodEndingBalance = accountPeriodStartingBalance ?? 0;

                // Calculate the ending balance
                foreach (var line in accountPeriodLedgerTransactionLines)
                {
                    accountPeriodEndingBalance += _ledgerAccountService.IsIncrement(account.Type, line.IsDebit)
                        ? line.Amount
                        : -line.Amount;
                }

                // Create account balance if it does not exist in the database yet
                var accountBalance =
                    _repository.GetOne<LedgerAccountBalance>(b => b.LedgerAccountId == account.Id && b.Year == year) ??
                    new LedgerAccountBalance { LedgerAccountId = account.Id };

                SetAccountBalance(accountBalance, month, accountPeriodEndingBalance);
                _repository.Update(accountBalance, modifiedBy);
            }
        }

        private void SetAccountBalance(LedgerAccountBalance accountBalance, int month, decimal amount)
        {
            switch (month)
            {
                case 1:
                    accountBalance.Balance1 = amount;
                    break;
                case 2:
                    accountBalance.Balance2 = amount;
                    break;
                case 3:
                    accountBalance.Balance3 = amount;
                    break;
                case 4:
                    accountBalance.Balance4 = amount;
                    break;
                case 5:
                    accountBalance.Balance5 = amount;
                    break;
                case 6:
                    accountBalance.Balance6 = amount;
                    break;
                case 7:
                    accountBalance.Balance7 = amount;
                    break;
                case 8:
                    accountBalance.Balance8 = amount;
                    break;
                case 9:
                    accountBalance.Balance9 = amount;
                    break;
                case 10:
                    accountBalance.Balance10 = amount;
                    break;
                case 11:
                    accountBalance.Balance11 = amount;
                    break;
                case 12:
                    accountBalance.Balance12 = amount;
                    break;
                default:
                    break;
            }
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
                },
                new LedgerAccount
                {
                    AccountNumber =
                        _ledgerAccountService.GetNewAccountNumber(chartOfAccountsId, LedgerAccountGroup.CommonStock),
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
        #endregion
    }
}