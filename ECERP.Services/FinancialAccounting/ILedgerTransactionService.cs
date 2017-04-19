namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
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
        /// Gets ledger transactions of a ledger account in a given period
        /// </summary>
        /// <param name="ledgerAccountId">Ledger Account Identifier</param>
        /// <param name="from">From Date</param>
        /// <param name="to">To Date</param>
        /// <returns>Ledger Transactions</returns>
        IList<LedgerTransaction> GetLedgerAccountTransactions(int ledgerAccountId, DateTime from, DateTime to);

        /// <summary>
        /// Insert a ledger transaction
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        void InsertLedgerTransaction(LedgerTransaction ledgerTransaction);
    }
}