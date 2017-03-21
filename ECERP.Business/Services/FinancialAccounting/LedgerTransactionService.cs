namespace ECERP.Business.Services.FinancialAccounting
{
    using System;
    using System.Linq;
    using Abstract;
    using Abstract.FinancialAccounting;
    using Data.Abstract;
    using Exceptions;
    using Models.Entities.Companies;
    using Models.Entities.FinancialAccounting;

    public class LedgerTransactionService : ILedgerTransactionService
    {
        private readonly IRepository _repository;
        private readonly ISystemParameterService _systemParamaterService;

        #region Constructor
        public LedgerTransactionService(IRepository repository, ISystemParameterService systemParameterService)
        {
            _repository = repository;
            _systemParamaterService = systemParameterService;
        }
        #endregion

        #region Interface Methods
        public LedgerTransaction GetSingleById(int id)
        {
            return _repository.GetById<LedgerTransaction>(id);
        }

        public void CreateTransaction(LedgerTransaction transaction, int companyId, string createdBy)
        {
            var totalDebit = 0m;
            var totalCredit = 0m;

            DateTime currentPeriodStartDate;

            try
            {
                currentPeriodStartDate = _systemParamaterService.GetLedgerCurrentPeriodStartDate(companyId);
            }
            catch (Exception e)
            {
                throw new LedgerTransactionException(e.Message);
            }

            var isInDifferentPostingPeriod = transaction.PostingDate.Year != currentPeriodStartDate.Year ||
                                             transaction.PostingDate.Year == currentPeriodStartDate.Year &&
                                             transaction.PostingDate.Month != currentPeriodStartDate.Month;

            if (transaction.PostingDate > DateTime.Now || isInDifferentPostingPeriod)
                throw new LedgerTransactionInvalidPostingDateException("Invalid posting date.");

            if (transaction.TransactionLines.Count == 0)
                throw new LedgerTransactionException("There are no transaction lines.");

            var coaId = _repository.GetById<Company>(companyId).ChartOfAccounts.Id;

            foreach (var transactionLine in transaction.TransactionLines)
            {
                var ledgerTransactionLine = (LedgerTransactionLine) transactionLine;
                var ledgerAccount = ledgerTransactionLine.LedgerAccount;

                if (!ledgerAccount.ChartOfAccountsId.Equals(coaId))
                    throw new LedgerTransactionException(
                        "Ledger transaction lines contain account(s) with different Chart of Accounts.");

                if (LedgerAccountCountInLedgerTransaction(transaction, ledgerAccount) > 1)
                    throw new LedgerTransactionException(
                        "Ledger transaction contains multiple lines with same ledger account.");

                ledgerTransactionLine.CreatedBy = createdBy;
                if (ledgerTransactionLine.IsDebit) totalDebit += ledgerTransactionLine.Amount;
                else totalCredit += ledgerTransactionLine.Amount;
            }

            if (totalDebit != totalCredit)
                throw new LedgerTransactionException("Total debit amount is not equal to total credit amount.");

            _repository.Create(transaction, createdBy);
            _repository.Save();
        }
        #endregion

        #region Private Methods
        private static int LedgerAccountCountInLedgerTransaction(LedgerTransaction transaction, LedgerAccount account)
        {
            return transaction.TransactionLines.Count(line =>
            {
                var l = line as LedgerTransactionLine;
                return l != null && l.LedgerAccount.Name.Equals(account.Name);
            });
        }
        #endregion
    }
}