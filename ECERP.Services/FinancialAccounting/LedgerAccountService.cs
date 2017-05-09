namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
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
            var skip = pageIndex * pageSize;
            var pagedLedgerAccounts =
                _repository
                    .Get(filter, sortOrder, skip, pageSize, la => la.ChartOfAccounts.Company);
            var totalCount = _repository.GetCount(filter);
            return new PagedList<LedgerAccount>(pagedLedgerAccounts, pageIndex, pageSize, totalCount);
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
            if (!CommonHelper.GetFirstDigit((int) ledgerAccount.Group).Equals((int) ledgerAccount.Type))
                throw new ArgumentException("Ledger account group is not compatible with type");

            var coaLedgerAccounts =
                _repository.Get<LedgerAccount>(la => la.ChartOfAccountsId.Equals(ledgerAccount.ChartOfAccountsId));
            if (coaLedgerAccounts.Any(account => account.Name.ToLowerInvariant().Equals(ledgerAccount.Name.ToLowerInvariant())))
                throw new ArgumentException("Ledger account name already exists.");
            
            ledgerAccount.AccountNumber = GenerateNewAccountNumber(ledgerAccount.Group);
            _repository.Create(ledgerAccount);
            _repository.Save();
        }

        /// <summary>
        /// Gets the account balance for a given period
        /// </summary>
        /// <param name="ledgerAccountId">Ledger Account Identifier</param>
        /// <param name="year">Period Year</param>
        /// <param name="month">Period Month</param>
        /// <returns>Account balance</returns>
        public virtual decimal GetPeriodLedgerAccountBalance(int ledgerAccountId, int year, int month)
        {
            var ledgerAccountBalance =
                _repository.GetOne<LedgerAccountBalance>(x => x.LedgerAccountId == ledgerAccountId && x.Year == year);
            return ledgerAccountBalance == null ? 0 : ledgerAccountBalance.GetMonthBalance(month);
        }

        /// <summary>
        /// Gets account balance prior to a given date
        /// </summary>
        /// <param name="ledgerAccountId">Ledger Account Identifier</param>
        /// <param name="date">Date</param>
        /// <returns>Account balance</returns>
        public virtual decimal GetLedgerAccountBalance(int ledgerAccountId, DateTime date)
        {
            var previousPeriodYear = date.Month == 1 ? date.Year - 1 : date.Year;
            var previousPeriodMonth = date.Month == 12 ? 1 : date.Month - 1;
            var periodBeginningBalance = GetPeriodLedgerAccountBalance(ledgerAccountId, previousPeriodYear,
                previousPeriodMonth);
            var periodLedgerTransactionLinesPriorToDate =
                _repository.Get<LedgerTransactionLine>(line => line.LedgerAccountId.Equals(ledgerAccountId) &&
                                                               line.LedgerTransaction.PostingDate.Year.Equals(date.Year) &&
                                                               line.LedgerTransaction.PostingDate.Month.Equals(
                                                                   date.Month) &&
                                                               line.LedgerTransaction.PostingDate.Date < date.Date,
                    null, null, null, line => line.LedgerAccount).ToList();
            if (!periodLedgerTransactionLinesPriorToDate.Any()) return periodBeginningBalance;
            return periodBeginningBalance +
                   periodLedgerTransactionLinesPriorToDate.Sum(line => line.IsIncrement() ? line.Amount : -line.Amount);
        }

        /// <summary>
        /// Generates a new account number
        /// </summary>
        /// <param name="group">Ledger Account Group</param>
        /// <returns>Account Number</returns>
        public int GenerateNewAccountNumber(LedgerAccountGroup group)
        {
            var defaultAccountNumber = group > 0 ? (int) group * 10000 + 1 : (int) group * 10000 - 1;

            var lastGroupLedgerAccount = group > 0
                ? _repository.Get<LedgerAccount>(la => la.Group.Equals(group))
                    .OrderByDescending(la => la.AccountNumber)
                    .FirstOrDefault()
                : _repository.Get<LedgerAccount>(la => la.Group.Equals(group))
                    .OrderBy(la => la.AccountNumber)
                    .FirstOrDefault();

            return lastGroupLedgerAccount == null
                ? defaultAccountNumber
                : group > 0 ? lastGroupLedgerAccount.AccountNumber + 1 : lastGroupLedgerAccount.AccountNumber - 1;
        }
        #endregion
    }
}