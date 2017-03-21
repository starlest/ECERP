namespace ECERP.Business.Services.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract.FinancialAccounting;
    using Data.Abstract;
    using Models.Entities.FinancialAccounting;

    public class LedgerAccountService : ILedgerAccountService
    {
        #region Private Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public LedgerAccountService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Interface Methods
        public IEnumerable<LedgerAccount> GetAll()
        {
            return _repository.GetAll<LedgerAccount>();
        }

        public IEnumerable<LedgerAccount> GetAllByCompany(string company)
        {
            return _repository.Get<LedgerAccount>(la => la.ChartOfAccounts.Company.Name.Equals(company));
        }

        public LedgerAccount GetSingleById(int id)
        {
            return _repository.GetById<LedgerAccount>(id);
        }

        public LedgerAccount GetSingleByName(int coaId, string name)
        {
            return
                _repository.GetOne<LedgerAccount>(
                    la => la.ChartOfAccountsId.Equals(coaId) && la.Name.Equals(name));
        }

        // TODO: Test Case
        public decimal? GetPeriodBalance(LedgerAccount ledgerAccount, int year, int month)
        {
            var periodAccountBalance =
                _repository.GetOne<LedgerAccountBalance>(
                    ab => ab.LedgerAccountId.Equals(ledgerAccount.Id) && ab.Year.Equals(year));
            if (periodAccountBalance == null) return null;
            switch (month)
            {
                case 0:
                    return periodAccountBalance.BeginningBalance;
                case 1:
                    return periodAccountBalance.Balance1;
                case 2:
                    return periodAccountBalance.Balance2;
                case 3:
                    return periodAccountBalance.Balance3;
                case 4:
                    return periodAccountBalance.Balance4;
                case 5:
                    return periodAccountBalance.Balance5;
                case 6:
                    return periodAccountBalance.Balance6;
                case 7:
                    return periodAccountBalance.Balance7;
                case 8:
                    return periodAccountBalance.Balance8;
                case 9:
                    return periodAccountBalance.Balance9;
                case 10:
                    return periodAccountBalance.Balance10;
                case 11:
                    return periodAccountBalance.Balance11;
                default:
                    return periodAccountBalance.Balance12;
            }
        }

        // TODO: Test Case and implementation
        public decimal GetCurrentBalance(LedgerAccount ledgerAccount)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var previousMonth = month != 1 ? month - 1 : 12;

            // Get the starting balance for this period
            var startingBalance = previousMonth == 12
                ? GetPeriodBalance(ledgerAccount, year - 1, previousMonth)
                : GetPeriodBalance(ledgerAccount, year, previousMonth);

            var currentBalance = startingBalance ?? 0;

            var periodLedgerTransactionLines =
                _repository.Get<LedgerTransactionLine>(l => l.LedgerAccountId.Equals(ledgerAccount.Id));

            currentBalance +=
                periodLedgerTransactionLines.Sum(
                    line => IsIncrement(ledgerAccount.Type, line.IsDebit) ? line.Amount : -line.Amount);

            return currentBalance;
        }

        public int GetNewAccountNumber(int chartOfAccountsId, LedgerAccountGroup group)
        {
            var accountNumber = (int) group * 10000 + 1;
            var lastAccount = _repository
                .Get<LedgerAccount>(
                    la => la.ChartOfAccountsId.Equals(chartOfAccountsId) && la.Group.Equals(group),
                    accounts => accounts.OrderByDescending(account => account.AccountNumber))
                .FirstOrDefault();
            return lastAccount?.AccountNumber + 1 ?? accountNumber;
        }

        public void Create(string name, string description, bool isActive, LedgerAccountType type,
            LedgerAccountGroup group, int chartOfAccountsId, string createdBy)
        {
            var ledgerAccount = new LedgerAccount
            {
                AccountNumber = GetNewAccountNumber(chartOfAccountsId, group),
                Name = name,
                Description = description,
                IsActive = true,
                IsDefault = false,
                Type = type,
                Group = group,
                ChartOfAccountsId = chartOfAccountsId
            };
            _repository.Create(ledgerAccount, createdBy);
            _repository.Save();
        }
        #endregion

        #region Helper Methods
        // Determines if for a given ledger account type, a debit/credit would cause an increment to the account balance
        public bool IsIncrement(LedgerAccountType type, bool isDebit)
        {
            bool isIncrement;

            if (isDebit)
            {
                isIncrement = type == LedgerAccountType.Asset ||
                              type == LedgerAccountType.ContraLiability ||
                              type == LedgerAccountType.ContraEquity ||
                              type == LedgerAccountType.ContraRevenue;
            }
            else
            {
                isIncrement = type == LedgerAccountType.Liability ||
                              type == LedgerAccountType.Equity ||
                              type == LedgerAccountType.Revenue ||
                              type == LedgerAccountType.ContraAsset ||
                              type == LedgerAccountType.ContraExpense;
            }

            return isIncrement;
        }
        #endregion
    }
}