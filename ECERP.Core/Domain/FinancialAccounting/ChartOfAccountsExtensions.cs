namespace ECERP.Core.Domain.FinancialAccounting
{
    public static class ChartOfAccountsExtensions
    {
        /// <summary>
        /// Advances ledger period to next month
        /// </summary>
        /// <param name="coa">Chart of accounts</param>
        public static void AdvanceLedgerPeriod(this ChartOfAccounts coa)
        {
            coa.CurrentLedgerPeriodStartDate = coa.CurrentLedgerPeriodStartDate.AddMonths(1);
        }

        /// <summary>
        /// Reverts ledger period to previous month
        /// </summary>
        /// <param name="coa">Chart of accounts</param>
        public static void RegressLedgerPeriod(this ChartOfAccounts coa)
        {
            coa.CurrentLedgerPeriodStartDate = coa.CurrentLedgerPeriodStartDate.AddMonths(-1);
        }
    }
}