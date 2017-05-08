namespace ECERP.Services.FinancialAccounting
{
    using System.Collections.Generic;
    using Core.Domain.FinancialAccounting;

    public interface IChartOfAccountsService
    {
        /// <summary>
        /// Gets a chart of accounts
        /// </summary>
        /// <param name="id">Chart of accounts identifier</param>
        /// <returns>Chart of accounts</returns>
        ChartOfAccounts GetChartOfAccountsById(int id);

        /// <summary>
        /// Regresses a chart of accounts current ledger period
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        void RegressLedgerPeriod(int coaId);

        /// <summary>
        /// Closes a chart of accounts current ledger period
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        void CloseLedgerPeriod(int coaId);

        /// <summary>
        /// Gets the balance for a given period
        /// </summary>
        /// <param name="coaId">Chart of Accounts Identifier</param>
        /// <param name="year">Period Year</param>
        /// <param name="month">Period Month</param>
        /// <returns>Balance Sheet</returns>
        IList<LedgerBalanceSheetItem> GetBalanceSheet(int coaId, int year, int month);
    }
}
