namespace ECERP.Core.Domain.FinancialAccounting
{
    public static class LedgerTransactionLineExtensions
    {
        /// <summary>
        /// Determines if this line increases or decreases its associated account balance
        /// </summary>
        /// <param name="ledgerTransactionLine">Ledger transaction line</param>
        /// <returns>true - increases, false - decreases</returns>
        public static bool IsIncrement(this LedgerTransactionLine ledgerTransactionLine)
        {
            var isDebit = ledgerTransactionLine.IsDebit;
            var type = ledgerTransactionLine.LedgerAccount.Type;
            return isDebit &&
                   (type == LedgerAccountType.Asset || type == LedgerAccountType.Expense ||
                    type == LedgerAccountType.ContraLiability || type == LedgerAccountType.ContraEquity ||
                    type == LedgerAccountType.ContraRevenue) ||
                   !isDebit &&
                   (type == LedgerAccountType.Liability || type == LedgerAccountType.Equity ||
                    type == LedgerAccountType.Revenue || type == LedgerAccountType.ContraAsset ||
                    type == LedgerAccountType.ContraExpense);
        }
    }
}