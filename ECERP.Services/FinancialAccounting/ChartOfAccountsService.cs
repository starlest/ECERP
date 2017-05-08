namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.FinancialAccounting;
    using Data.Abstract;

    /// <summary>
    /// Chart of Accounts Service
    /// </summary>
    public class ChartOfAccountsService : IChartOfAccountsService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public ChartOfAccountsService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a chart of accounts
        /// </summary>
        /// <param name="id">Chart of accounts identifier</param>
        /// <returns>Chart of accounts</returns>
        public virtual ChartOfAccounts GetChartOfAccountsById(int id)
        {
            return _repository.GetById<ChartOfAccounts>(id, coa => coa.LedgerAccounts);
        }

        /// <summary>
        /// Regresses a chart of accounts current ledger period
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        public virtual void RegressLedgerPeriod(int coaId)
        {
            var coa = GetChartOfAccountsById(coaId);
            if (coa == null)
                throw new ArgumentException("Chart of accounts does not exist.");
            coa.RegressLedgerPeriod();

            // Remove previous closing entries
            var year = coa.CurrentLedgerPeriodStartDate.Year;
            var month = coa.CurrentLedgerPeriodStartDate.Month;
            var closingEntries =
                _repository.Get<LedgerTransaction>(
                    lt =>
                        lt.ChartOfAccountsId.Equals(coaId) &&
                        lt.IsClosing &&
                        lt.PostingDate.Year.Equals(year) &&
                        lt.PostingDate.Month.Equals(month));
            foreach (var entry in closingEntries)
            {
                _repository.Delete(entry);
            }

            _repository.Save();
        }

        /// <summary>
        /// Closes a chart of accounts current ledger period
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        public virtual void CloseLedgerPeriod(int coaId)
        {
            var coa = GetChartOfAccountsById(coaId);
            if (coa == null)
                throw new ArgumentException("Chart of accounts does not exist.");

            var year = coa.CurrentLedgerPeriodStartDate.Year;
            var month = coa.CurrentLedgerPeriodStartDate.Month;

            // Close expense and revenue accounts to retained earnings
            var expenseRevenueAccounts = GetExpenseRevenueAccounts(coa.LedgerAccounts);
            var retainedEarningsAccount = GetRetainedEarningsAccount(coa.LedgerAccounts);
            var amountClosedToRetainedEarnings = 0m;

            foreach (var account in expenseRevenueAccounts)
            {
                var closingAmount = CalculatePeriodLedgerAccountBalanceChange(account, year, month);

                if (closingAmount == 0) continue;

                var closingEntry = new LedgerTransaction
                {
                    Documentation = "Closing Entry",
                    Description = "Closing Entry",
                    PostingDate = coa.CurrentLedgerPeriodStartDate.AddMonths(1).AddDays(-1), // last day of the month
                    IsEditable = false,
                    IsClosing = true,
                    ChartOfAccountsId = coaId
                };

                var closingEntryLine = new LedgerTransactionLine
                {
                    LedgerAccountId = account.Id,
                    Amount = closingAmount,
                    IsDebit =
                        account.Type.Equals(LedgerAccountType.Revenue) ||
                        account.Type.Equals(LedgerAccountType.ContraExpense)
                };

                // take into account if the amount is negative
                closingEntryLine.IsDebit = closingAmount < 0 ? !closingEntryLine.IsDebit : closingEntryLine.IsDebit;

                var closingEntryRetainedEarningsLine = new LedgerTransactionLine
                {
                    LedgerAccountId = retainedEarningsAccount.Id,
                    Amount = closingAmount,
                    IsDebit = !closingEntryLine.IsDebit
                };

                amountClosedToRetainedEarnings += closingEntryRetainedEarningsLine.IsDebit
                    ? -closingEntryRetainedEarningsLine.Amount
                    : closingEntryRetainedEarningsLine.Amount;

                closingEntry.LedgerTransactionLines.Add(closingEntryLine);
                closingEntry.LedgerTransactionLines.Add(closingEntryRetainedEarningsLine);

                _repository.Create(closingEntry);
            }

            var balanceSheetAccounts = GetBalanceSheetLedgerAccounts(coa.LedgerAccounts);

            foreach (var account in balanceSheetAccounts)
            {
                bool isCreate;
                var accountBalance = GetPeriodLedgerAccountBalance(account.Id, year, month, out isCreate);

                // Get the period starting balance
                var accountPeriodStartingBalance = accountBalance.GetMonthBalance(month - 1);

                // Calculate the period ending balance
                decimal accountPeriodEndingBalance;

                if (!account.Name.Equals("Retained Earnings"))
                    accountPeriodEndingBalance = accountPeriodStartingBalance +
                                                 CalculatePeriodLedgerAccountBalanceChange(account, year, month);
                else
                    accountPeriodEndingBalance = accountPeriodStartingBalance + amountClosedToRetainedEarnings;

                // Update the ending balance
                accountBalance.SetMonthBalance(month, accountPeriodEndingBalance);

                if (isCreate)
                    _repository.Create(accountBalance);
                else
                    _repository.Update(accountBalance);
            }

            // Advance and update the current ledger period
            coa.AdvanceLedgerPeriod();
            _repository.Update(coa);

            // Persist changes
            _repository.Save();
        }

        /// <summary>
        /// Gets the balance for a given period
        /// </summary>
        /// <param name="coaId">Chart of Accounts Identifier</param>
        /// <param name="year">Period Year</param>
        /// <param name="month">Period Month</param>
        /// <returns>Balance Sheet</returns>
        public virtual IList<LedgerBalanceSheetItem> GetBalanceSheet(int coaId, int year, int month)
        {
            var coa = GetChartOfAccountsById(coaId);
            if (coa == null)
                throw new ArgumentException("Chart of accounts does not exist.");

            var balanceSheetItems = new List<LedgerBalanceSheetItem>();

            var groups = Enum.GetValues(typeof(LedgerAccountGroup));

            foreach (var group in groups)
            {
                var balanceSheetItem = new LedgerBalanceSheetItem
                {
                    Name = group.ToString(),
                    Amount = 0
                };

                balanceSheetItems.Add(balanceSheetItem);

                var groupLedgerAccounts = coa.LedgerAccounts.Where(la => la.Group.Equals(group)).ToList();

                if (!groupLedgerAccounts.Any()) continue;

                foreach (var groupLedgerAccount in groupLedgerAccounts)
                {
                    var periodLedgerAccountBalance =
                        _repository.GetOne<LedgerAccountBalance>(
                            lab => lab.LedgerAccountId.Equals(groupLedgerAccount.Id) && lab.Year.Equals(year));
                    if (periodLedgerAccountBalance == null) continue;
                    balanceSheetItem.Amount += periodLedgerAccountBalance.GetMonthBalance(month);
                }
            }

            return balanceSheetItems;
        }
        #endregion

        #region Utilities
        private static LedgerAccount GetRetainedEarningsAccount(IEnumerable<LedgerAccount> LedgerAccounts)
        {
            return LedgerAccounts.Single(account => account.Name.Equals("Retained Earnings"));
        }

        private static IEnumerable<LedgerAccount> GetBalanceSheetLedgerAccounts(
            IEnumerable<LedgerAccount> ledgerAccounts)
        {
            return ledgerAccounts.Where(account => account.Type.Equals(LedgerAccountType.Asset) ||
                                                   account.Type.Equals(LedgerAccountType.Liability) ||
                                                   account.Type.Equals(LedgerAccountType.Equity) ||
                                                   account.Type.Equals(LedgerAccountType.ContraAsset) ||
                                                   account.Type.Equals(LedgerAccountType.ContraLiability) ||
                                                   account.Type.Equals(LedgerAccountType.ContraEquity));
        }

        private static IEnumerable<LedgerAccount> GetExpenseRevenueAccounts(IEnumerable<LedgerAccount> ledgerAccounts)
        {
            return ledgerAccounts.Where(account => account.Type.Equals(LedgerAccountType.Expense) ||
                                                   account.Type.Equals(LedgerAccountType.ContraExpense) ||
                                                   account.Type.Equals(LedgerAccountType.Revenue) ||
                                                   account.Type.Equals(LedgerAccountType.ContraRevenue));
        }

        private decimal CalculatePeriodLedgerAccountBalanceChange(LedgerAccount ledgerAccount, int year, int month)
        {
            var periodLedgerTransactionLines = _repository.Get<LedgerTransactionLine>(line =>
                line.LedgerAccountId.Equals(ledgerAccount.Id) &&
                line.LedgerTransaction.PostingDate.Year.Equals(year) &&
                line.LedgerTransaction.PostingDate.Month.Equals(month));
            return LedgerAccountBalanceExtensions.CalculateLedgerTransactionLinesTotal(periodLedgerTransactionLines);
        }

        private LedgerAccountBalance GetPeriodLedgerAccountBalance(int ledgerAccountId, int year, int month,
            out bool isCreate)
        {
            isCreate = false;
            LedgerAccountBalance accountBalance;

            // If closing month is January, create a new account balance for this year
            if (month == 1)
            {
                isCreate = true;
                accountBalance = new LedgerAccountBalance { LedgerAccountId = ledgerAccountId, Year = year };

                var previousYearAccountBalance =
                    _repository.GetOne<LedgerAccountBalance>(
                        b => b.LedgerAccountId == ledgerAccountId && b.Year == year - 1);

                // Set the beginning balance to previous year's ending balance
                if (previousYearAccountBalance != null)
                    accountBalance.SetMonthBalance(0, previousYearAccountBalance.Balance12);

                return accountBalance;
            }

            accountBalance =
                _repository.GetOne<LedgerAccountBalance>(b => b.LedgerAccountId == ledgerAccountId && b.Year == year);

            // Create account balance if it does not exist in the database yet
            if (accountBalance == null)
            {
                isCreate = true;
                accountBalance = new LedgerAccountBalance { LedgerAccountId = ledgerAccountId, Year = year };
            }

            return accountBalance;
        }
        #endregion
    }
}