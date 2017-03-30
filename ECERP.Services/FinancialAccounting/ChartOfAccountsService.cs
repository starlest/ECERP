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
            return _repository.GetById<ChartOfAccounts>(id);
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

            // Filter out only the accounts that are required for closing
            var accounts = ExtractClosingLedgerAccounts(coa.LedgerAccounts);

            foreach (var account in accounts)
            {
                var accountBalance = GetPeriodLedgerAccountBalance(account.Id, year, month);

                // Get the period starting balance
                var accountPeriodStartingBalance = accountBalance.GetMonthBalance(month - 1);

                // Calculate the period ending balance
                var accountPeriodEndingBalance = accountPeriodStartingBalance +
                                                 CalculatePeriodLedgerAccountBalanceChange(account, year, month);

                // Update the ending balance
                accountBalance.SetMonthBalance(month, accountPeriodEndingBalance);
                _repository.Update(accountBalance);
            }

            // Advance and update the current ledger period
            coa.AdvanceLedgerPeriod();
            _repository.Update(coa);

            // Persist changes
            _repository.Save();
        }
        #endregion

        #region Utilities
        private static IEnumerable<LedgerAccount> ExtractClosingLedgerAccounts(IEnumerable<LedgerAccount> ledgerAccounts)
        {
            return ledgerAccounts.Where(account => account.Type.Equals(LedgerAccountType.Asset) ||
                                                   account.Type.Equals(LedgerAccountType.Liability) ||
                                                   account.Type.Equals(LedgerAccountType.Equity) ||
                                                   account.Type.Equals(LedgerAccountType.ContraAsset) ||
                                                   account.Type.Equals(LedgerAccountType.ContraLiability) ||
                                                   account.Type.Equals(LedgerAccountType.ContraEquity));
        }

        private decimal CalculatePeriodLedgerAccountBalanceChange(LedgerAccount ledgerAccount, int year, int month)
        {
            var periodLedgerTransactionLines = _repository.Get<LedgerTransactionLine>(line =>
                line.LedgerAccountId.Equals(ledgerAccount.Id) &&
                line.LedgerTransaction.PostingDate.Year.Equals(year) &&
                line.LedgerTransaction.PostingDate.Month.Equals(month));
            return LedgerAccountBalanceExtensions.CalculateLedgerTransactionLinesTotal(periodLedgerTransactionLines);
        }

        private LedgerAccountBalance GetPeriodLedgerAccountBalance(int ledgerAccountId, int year, int month)
        {
            // If closing month is January, create a new account balance for this year
            if (month == 1)
            {
                var accountBalance = new LedgerAccountBalance { LedgerAccountId = ledgerAccountId, Year = year };

                var previousYearAccountBalance =
                    _repository.GetOne<LedgerAccountBalance>(
                        b => b.LedgerAccountId == ledgerAccountId && b.Year == year - 1);
                // Set the beginning balance to previous year's ending balance
                if (previousYearAccountBalance != null)
                    accountBalance.SetMonthBalance(0, previousYearAccountBalance.Balance12);

                return accountBalance;
            }

            // Create account balance if it does not exist in the database yet
            return
                _repository.GetOne<LedgerAccountBalance>(b => b.LedgerAccountId == ledgerAccountId && b.Year == year) ??
                new LedgerAccountBalance { LedgerAccountId = ledgerAccountId, Year = year };
        }
        #endregion
    }
}