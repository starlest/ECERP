namespace ECERP.Services.FinancialAccounting
{
    using System;
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
        private readonly ILedgerAccountService _ledgerAccountService;
        #endregion

        #region Constructor
        public ChartOfAccountsService(IRepository repository, ILedgerAccountService ledgerAccountService)
        {
            _repository = repository;
            _ledgerAccountService = ledgerAccountService;
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

            // filter out the accounts that are required for closing
            var accounts = coa.LedgerAccounts.Where(
                account => account.Type.Equals(LedgerAccountType.Asset) &&
                           account.Type.Equals(LedgerAccountType.Liability) &&
                           account.Type.Equals(LedgerAccountType.Equity) &&
                           account.Type.Equals(LedgerAccountType.ContraAsset) &&
                           account.Type.Equals(LedgerAccountType.ContraLiability) &&
                           account.Type.Equals(LedgerAccountType.ContraEquity));

            foreach (var account in accounts)
            {
                var accountPeriodLedgerTransactionLines = _repository.Get<LedgerTransactionLine>(
                        line =>
                            line.LedgerAccountId.Equals(account.Id) &&
                            line.LedgerTransaction.PostingDate.Year.Equals(year) &&
                            line.LedgerTransaction.PostingDate.Month.Equals(month))
                    .ToList();

                // Get the starting balance of the account
                var accountPeriodStartingBalance = month == 1
                    ? _ledgerAccountService.GetPeriodLedgerAccountBalance(account, year, 0)
                    : _ledgerAccountService.GetPeriodLedgerAccountBalance(account, year, month);

                // Calculate the ending balance
                var accountPeriodEndingBalance = accountPeriodStartingBalance +
                                                 accountPeriodLedgerTransactionLines.Sum(
                                                     line =>
                                                         IsIncrement(account.Type, line.IsDebit)
                                                             ? line.Amount
                                                             : -line.Amount);

                // Create account balance if it does not exist in the database yet
                var accountBalance =
                    _repository.GetOne<LedgerAccountBalance>(b => b.LedgerAccountId == account.Id && b.Year == year) ??
                    new LedgerAccountBalance { LedgerAccountId = account.Id };

                accountBalance.SetMonthBalance(month, accountPeriodEndingBalance);
                _repository.Update(accountBalance);
            }

            coa.AdvanceLedgerPeriod();
            _repository.Save();
        }
        #endregion

        #region Utilities
        private static bool IsIncrement(LedgerAccountType type, bool isDebit)
        {
            return isDebit &&
                   (type == LedgerAccountType.Asset || type == LedgerAccountType.Expense ||
                    type == LedgerAccountType.ContraLiability || type == LedgerAccountType.ContraEquity ||
                    type == LedgerAccountType.ContraRevenue) ||
                   !isDebit &&
                   (type == LedgerAccountType.Liability || type == LedgerAccountType.Equity ||
                    type == LedgerAccountType.Revenue || type == LedgerAccountType.ContraAsset ||
                    type == LedgerAccountType.ContraExpense);
        }
        #endregion
    }
}