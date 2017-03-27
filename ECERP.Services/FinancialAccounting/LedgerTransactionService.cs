namespace ECERP.Services.FinancialAccounting
{
    using System;
    using Configuration;
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
        public LedgerTransaction GetLedgerTransactionById(int id)
        {
            return _repository.GetById<LedgerTransaction>(id);
        }

        /// <summary>
        /// Insert a ledger transaction
        /// </summary>
        /// <param name="ledgerTransaction">Ledger transaction</param>
        public void InsertLedgerTransaction(LedgerTransaction ledgerTransaction)
        {
            if (ledgerTransaction.LedgerTransactionLines.Count <= 1)
                throw new ArgumentException("There are no ledger transaction lines.");

            if (ledgerTransaction.AreThereAccountsFromDifferentCOAs())
                throw new ArgumentException("Ledger transaction lines contains ledger accounts from different chart of accounts.");

            var currentLedgerPeriodStartingDate = ledgerTransaction.ChartOfAccounts.CurrentLedgerPeriodStartDate;

            if (ledgerTransaction.PostingDate.Year != currentLedgerPeriodStartingDate.Year ||
                ledgerTransaction.PostingDate.Month != currentLedgerPeriodStartingDate.Month)
                throw new ArgumentException("Posting period is invalid.");

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