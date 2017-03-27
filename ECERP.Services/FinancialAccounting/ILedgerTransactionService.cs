namespace ECERP.Services.FinancialAccounting
{
    using Core.Domain.FinancialAccounting;

    /// <summary>
    /// Ledger transaction service
    /// </summary>
    public interface ILedgerTransactionService
    {
        /// <summary>
        /// Gets a ledger transaction
        /// </summary>
        /// <param name="id">Ledger transaction identifier</param>
        /// <returns>Ledger transaction</returns>
        LedgerTransaction GetLedgerTransactionById(int id);

        /// <summary>
        /// Insert a ledger transaction
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        void InsertLedgerTransaction(LedgerTransaction ledgerTransaction);
    }
}