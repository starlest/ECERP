namespace ECERP.Services.FinancialAccounting
{
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
    }
}
