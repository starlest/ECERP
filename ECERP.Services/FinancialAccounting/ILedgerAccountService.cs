namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.FinancialAccounting;

    /// <summary>
    /// Ledger account service
    /// </summary>
    public interface ILedgerAccountService
    {
        /// <summary>
        /// Gets ledger accounts
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Ledger Accounts</returns>
        IPagedList<LedgerAccount> GetLedgerAccounts(
            Expression<Func<LedgerAccount, bool>> filter = null,
            Func<IQueryable<LedgerAccount>, IOrderedQueryable<LedgerAccount>> sortOrder = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a ledger account
        /// </summary>
        /// <param name="id">Ledger Account Identifier</param>
        /// <returns>Ledger Account</returns>
        LedgerAccount GetLedgerAccountById(int id);

        /// <summary>
        /// Gets a ledger account
        /// </summary>
        /// <param name="coaId">Chart of Accounts Identifier</param>
        /// <param name="name">Name</param>
        /// <returns>Ledger Account</returns>
        LedgerAccount GetLedgerAccountByName(int coaId, string name);

        /// <summary>
        /// Inserts a ledger account
        /// </summary>
        /// <param name="ledgerAccount">Ledger Account</param>
        void InsertLedgerAccount(LedgerAccount ledgerAccount);

        /// <summary>
        /// Gets account balance for a given period
        /// </summary>
        /// <param name="ledgerAccountId">Ledger Account Identifier</param>
        /// <param name="year">Period Year</param>
        /// <param name="month">Period Month</param>
        /// <returns>Account balance</returns>
        decimal GetPeriodLedgerAccountBalance(int ledgerAccountId, int year, int month);


        /// <summary>
        /// Gets account balance on a given date
        /// </summary>
        /// <param name="ledgerAccountId">Ledger Account Identifier</param>
        /// <param name="date">Date</param>
        /// <returns>Account balance</returns>
        decimal GetLedgerAccountBalance(int ledgerAccountId, DateTime date);

        /// <summary>
        /// Generates a new account number
        /// </summary>
        /// <param name="group">Ledger Account Group</param>
        /// <returns>Account Number</returns>
        int GenerateNewAccountNumber(LedgerAccountGroup group);
    }
}