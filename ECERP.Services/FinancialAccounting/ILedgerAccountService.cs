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
        /// <returns>Ledger accounts</returns>
        IPagedList<LedgerAccount> GetLedgerAccounts(
            Expression<Func<LedgerAccount, bool>> filter = null,
            Func<IQueryable<LedgerAccount>, IOrderedQueryable<LedgerAccount>> sortOrder = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a ledger account
        /// </summary>
        /// <param name="id">Ledger account identifier</param>
        /// <returns>Ledger account</returns>
        LedgerAccount GetLedgerAccountById(int id);

        /// <summary>
        /// Gets a ledger account
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        /// <param name="name">Ledger account name</param>
        /// <returns>Ledger account</returns>
        LedgerAccount GetLedgerAccountByName(int coaId, string name);

        /// <summary>
        /// Insert a ledger account
        /// </summary>
        /// <param name="ledgerAccount">Ledger account</param>
        void InsertLedgerAccount(LedgerAccount ledgerAccount);

        /// <summary>
        /// Gets account balance for a given period
        /// </summary>
        /// <param name="ledgerAccount">Ledger account</param>
        /// <param name="year">Period year</param>
        /// <param name="month">Period month</param>
        /// <returns>Account balance</returns>
        decimal GetPeriodLedgerAccountBalance(LedgerAccount ledgerAccount, int year, int month);

        /// <summary>
        /// Generates a new account number
        /// </summary>
        /// <param name="group">Ledger Account Group</param>
        /// <returns>Account Number</returns>
        int GenerateNewAccountNumber(LedgerAccountGroup group);
    }
}