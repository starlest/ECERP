namespace ECERP.Services.FinancialAccounting
{
    using System.Collections.Generic;
    using Core.Domain.FinancialAccounting;

    /// <summary>
    /// Ledger account service
    /// </summary>
    public interface ILedgerAccountService
    {
        /// <summary>
        /// Gets all chart of accounts ledger accounts
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        /// <returns>Ledger accounts</returns>
        IList<LedgerAccount> GetAllLedgerAccountsByCOAId(int coaId);

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

//        decimal GetCurrentBalance(LedgerAccount ledgerAccount);
//        int GetNewAccountNumber(int chartOfAccountsId, LedgerAccountGroup group);
//
//        bool IsIncrement(LedgerAccountType type, bool isDebit);
    }
}