namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using Core.Domain.FinancialAccounting;
    using Data.Abstract;

    /// <summary>
    /// Ledger account service
    /// </summary>
    public class LedgerAccountService : ILedgerAccountService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public LedgerAccountService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets all chart of accounts ledger accounts
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        /// <returns>Ledger accounts</returns>
        public virtual IList<LedgerAccount> GetAllLedgerAccountsByCOAId(int coaId)
        {
            return _repository.GetById<ChartOfAccounts>(coaId).LedgerAccounts;
        }

        /// <summary>
        /// Gets a ledger account
        /// </summary>
        /// <param name="id">Ledger account identifier</param>
        /// <returns>Ledger account</returns>
        public virtual LedgerAccount GetLedgerAccountById(int id)
        {
            return _repository.GetById<LedgerAccount>(id);
        }

        /// <summary>
        /// Gets a ledger account
        /// </summary>
        /// <param name="coaId">Chart of accounts identifier</param>
        /// <param name="name">Ledger account name</param>
        /// <returns>Ledger account</returns>
        public virtual LedgerAccount GetLedgerAccountByName(int coaId, string name)
        {
            return _repository.GetOne<LedgerAccount>(x => x.Name.Equals(name) && x.ChartOfAccountsId.Equals(coaId));
        }

        /// <summary>
        /// Insert a ledger account
        /// </summary>
        /// <param name="ledgerAccount">Ledger account</param>
        public virtual void InsertLedgerAccount(LedgerAccount ledgerAccount)
        {
            _repository.Create(ledgerAccount);
            _repository.Save();
        }

        /// <summary>
        /// Gets the account balance for a given period
        /// </summary>
        /// <param name="ledgerAccount">Ledger account</param>
        /// <param name="year">Period year</param>
        /// <param name="month">Period month</param>
        /// <returns>Account balance</returns>
        public virtual decimal GetPeriodLedgerAccountBalance(LedgerAccount ledgerAccount, int year, int month)
        {
            var ledgerAccountBalance =
                _repository.GetOne<LedgerAccountBalance>(x => x.LedgerAccountId == ledgerAccount.Id && x.Year == year);
            return ledgerAccountBalance == null ? 0 : ledgerAccountBalance.GetMonthBalance(month);
        }
        #endregion
    }
}