namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using Core;
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
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Ledger Transactions</returns>
        IPagedList<LedgerTransaction> GetLedgerAccountTransactions(
            int ledgerAccountId,
            DateTime from, DateTime to,
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Insert a ledger transaction
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        void InsertLedgerTransaction(LedgerTransaction ledgerTransaction);

        /// <summary>
        /// Delete a ledger transaction
        /// </summary>
        /// <param name="ledgerTransactionId">Ledger Transaction Identifier</param>
        void DeleteLedgerTransaction(int ledgerTransactionId);
    }
}