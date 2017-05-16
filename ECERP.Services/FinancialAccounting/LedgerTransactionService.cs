namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Linq;
    using Core;
    using Core.Domain.FinancialAccounting;
    using Data.Abstract;

    public class LedgerTransactionService : ILedgerTransactionService
    {
        #region Fields
        private readonly IRepository _repository;
        #endregion

        #region Constructor
        public LedgerTransactionService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a ledger transaction
        /// </summary>
        /// <param name="id">Ledger transaction identifier</param>
        /// <returns>Ledger transaction</returns>
        public virtual LedgerTransaction GetLedgerTransactionById(int id)
        {
            return _repository.GetById<LedgerTransaction>(id, lt => lt.LedgerTransactionLines);
        }

        /// <summary>
        /// Gets ledger transactions of a ledger account in a given period
        /// </summary>
        /// <param name="ledgerAccountId">Ledger Account Identifier</param>
        /// <param name="from">From Date</param>
        /// <param name="to">To Date</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Ledger Transactions</returns>
        public virtual IPagedList<LedgerTransaction> GetLedgerAccountTransactions(
            int ledgerAccountId,
            DateTime from,
            DateTime to,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            var skip = pageIndex * pageSize;
            var pagedLines =
                _repository.Get<LedgerTransactionLine>(
                    line =>
                        line.LedgerAccountId.Equals(ledgerAccountId) &&
                        line.LedgerTransaction.PostingDate >= from &&
                        line.LedgerTransaction.PostingDate <= to,
                    x => x.OrderBy(line => line.LedgerTransaction.PostingDate),
                    skip,
                    pageSize,
                    line => line.LedgerTransaction, line => line.LedgerTransaction.LedgerTransactionLines);
            var totalCount = _repository.GetCount<LedgerTransactionLine>(line =>
                line.LedgerAccountId.Equals(ledgerAccountId) &&
                line.LedgerTransaction.PostingDate >= from &&
                line.LedgerTransaction.PostingDate <= to);
            var pagedLedgerTransactions = pagedLines.Select(line => line.LedgerTransaction).ToList();
            return new PagedList<LedgerTransaction>(pagedLedgerTransactions, pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// Insert a ledger transaction
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        public virtual void InsertLedgerTransaction(LedgerTransaction ledgerTransaction)
        {
            if (ledgerTransaction.LedgerTransactionLines.Count <= 1)
                throw new ArgumentException("There are no ledger transaction lines.");

            // Attach properties
            ledgerTransaction.ChartOfAccounts = _repository.GetById<ChartOfAccounts>(ledgerTransaction.ChartOfAccountsId);

            foreach (var line in ledgerTransaction.LedgerTransactionLines)
                line.LedgerAccount = _repository.GetById<LedgerAccount>(line.LedgerAccountId);

            if (ledgerTransaction.AreThereAccountsFromDifferentCOAs())
                throw new ArgumentException(
                    "Ledger transaction lines contains ledger accounts from different chart of accounts.");

            var currentLedgerPeriodStartingDate = ledgerTransaction.ChartOfAccounts.CurrentLedgerPeriodStartDate;

            if (ledgerTransaction.PostingDate.Year != currentLedgerPeriodStartingDate.Year ||
                ledgerTransaction.PostingDate.Month != currentLedgerPeriodStartingDate.Month ||
                ledgerTransaction.PostingDate > DateTime.Now)
                throw new ArgumentException("Invalid posting period.");

            if (ledgerTransaction.GetTotalDebitAmount() != ledgerTransaction.GetTotalCreditAmount())
                throw new ArgumentException("Total debit amount differs from total credit amount.");

            if (ledgerTransaction.AreThereDuplicateAccounts())
                throw new ArgumentException("Ledger transaction lines contain duplicate ledger accounts.");

            _repository.Create(ledgerTransaction);

            _repository.Save();
        }

        /// <summary>
        /// Delete a ledger transaction
        /// </summary>
        /// <param name="ledgerTransactionId">Ledger Transaction Identifier</param>
        public virtual void DeleteLedgerTransaction(int ledgerTransactionId)
        {
            var ledgerTransaction = _repository.GetById<LedgerTransaction>(ledgerTransactionId);
            if (ledgerTransaction == null)
                throw new ArgumentException("Ledger transaction does not exist in the database.");
            if (ledgerTransaction.PostingDate.Date != DateTime.Now.Date)
                throw new ArgumentException("Posting date is different from current date.");
            _repository.Delete(ledgerTransaction);
            _repository.Save();
        }
        #endregion
    }
}