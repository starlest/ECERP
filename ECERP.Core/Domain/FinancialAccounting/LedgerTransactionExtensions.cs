namespace ECERP.Core.Domain.FinancialAccounting
{
    using System.Linq;

    public static class LedgerTransactionExtensions
    {
        /// <summary>
        /// Gets ledger transaction's total debit amount
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        /// <returns>Ledger transaction total debit amount</returns>
        public static decimal GetTotalDebitAmount(this LedgerTransaction ledgerTransaction)
        {
            return ledgerTransaction.LedgerTransactionLines.Where(line => line.IsDebit).Sum(line => line.Amount);
        }

        /// <summary>
        /// Gets ledger transaction's total credit amount
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        /// <returns>Ledger transaction total credit amount</returns>
        public static decimal GetTotalCreditAmount(this LedgerTransaction ledgerTransaction)
        {
            return ledgerTransaction.LedgerTransactionLines.Where(line => !line.IsDebit).Sum(line => line.Amount);
        }

        /// <summary>
        /// Indicates whether there are duplicate accounts in the transaction lines
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        /// <returns>true - contains, false - no</returns>
        public static bool AreThereDuplicateAccounts(this LedgerTransaction ledgerTransaction)
        {
            var transactionLines = ledgerTransaction.LedgerTransactionLines;
            return
                transactionLines.Any(
                    line => transactionLines.Count(l => l.LedgerAccountId.Equals(line.LedgerAccountId)) > 1);
        }

        /// <summary>
        /// Indicates whether there are accounts from different charts of accounts
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        /// <returns>true - contains, false - no</returns>
        public static bool AreThereAccountsFromDifferentCOAs(this LedgerTransaction ledgerTransaction)
        {
            var transactionLines = ledgerTransaction.LedgerTransactionLines;
            return transactionLines.Any(line => line.LedgerAccount.ChartOfAccountsId != ledgerTransaction.ChartOfAccountsId);
        }
    }
}