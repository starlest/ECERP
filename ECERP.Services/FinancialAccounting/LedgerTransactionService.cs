namespace ECERP.Services.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        /// <returns>Ledger Transactions</returns>
        public virtual IList<LedgerTransaction> GetLedgerAccountTransactions(int ledgerAccountId, DateTime from,
            DateTime to)
        {
            var lines =
                _repository.Get<LedgerTransactionLine>(
                    line =>
                        line.LedgerAccountId.Equals(ledgerAccountId) && 
                        line.LedgerTransaction.PostingDate >= from &&
                        line.LedgerTransaction.PostingDate <= to,
                    x => x.OrderBy(line => line.LedgerTransaction.PostingDate),
                    null,
                    null,
                    line => line.LedgerTransaction);
            return lines.Select(line => line.LedgerTransaction).ToList();
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
        #endregion
    }
}