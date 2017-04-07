namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
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
        /// Gets ledger accounts
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Ledger accounts</returns>
        public virtual IPagedList<LedgerAccount> GetLedgerAccounts(
            Expression<Func<LedgerAccount, bool>> filter = null,
            Func<IQueryable<LedgerAccount>, IOrderedQueryable<LedgerAccount>> sortOrder = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            var skip = (pageIndex - 1) * pageSize;
            var customers =
                _repository.Get(filter, sortOrder, null, null, la => la.ChartOfAccounts.Company).ToList();
            var pagedCustomers = customers.Skip(skip).Take(pageSize);
            return new PagedList<LedgerAccount>(pagedCustomers, pageIndex, pageSize, customers.Count);
        }

        /// <summary>
        /// Gets a ledger account
        /// </summary>
        /// <param name="id">Ledger account identifier</param>
        /// <returns>Ledger account</returns>
        public virtual LedgerAccount GetLedgerAccountById(int id)
        {
            return _repository.GetById<LedgerAccount>(id, la => la.ChartOfAccounts.Company);
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

        /// <summary>
        /// Generates a new account number
        /// </summary>
        /// <param name="group">Ledger Account Group</param>
        /// <returns>Account Number</returns>
        public int GetNewAccountNumber(LedgerAccountGroup group)
        {
            var defaultAccountNumber = (int) group * 10000 + 1;

            var lastGroupLedgerAccount =
                _repository.Get<LedgerAccount>(la => la.Group.Equals(group))
                    .OrderByDescending(la => la.AccountNumber)
                    .FirstOrDefault();

            return lastGroupLedgerAccount == null ? defaultAccountNumber : lastGroupLedgerAccount.AccountNumber + 1;
        }
        #endregion
    }
}