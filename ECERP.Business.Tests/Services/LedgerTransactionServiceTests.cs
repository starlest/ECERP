namespace ECERP.Business.Tests.Services
{
    using System;
    using System.Linq;
    using Exceptions;
    using Models.Entities.FinancialAccounting;
    using Xunit;

    [Collection("Database collection")]
    public class LedgerTransactionServiceTests
    {
        private readonly ServiceFixture _fixture;

        #region Constructor
        public LedgerTransactionServiceTests(ServiceFixture fixture)
        {
            _fixture = fixture;
        }
        #endregion

        #region Test Cases
        [Fact]
        public void CreateLedgerTransactionTest()
        {
            var company = _fixture.CompanyService.GetAll().First();
            var otherCompany = _fixture.CompanyService.GetAll().Last();

            var cashLedgerAccount = _fixture.LedgerAccountService.GetSingleByName(company.ChartOfAccounts.Id, "Cash");
            var capitalLedgerAccount = _fixture.LedgerAccountService.GetSingleByName(company.ChartOfAccounts.Id,
                "Capital");

            var otherCompanyCapitalLedgerAccount =
                _fixture.LedgerAccountService.GetSingleByName(otherCompany.ChartOfAccounts.Id,
                    "Capital");

            var transaction = new LedgerTransaction
            {
                Documentation = "Test transaction 1",
                Description = "Test Transaction"
            };

            // No lines
            Assert.Throws<LedgerTransactionException>(
                () =>
                    _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin"));

            var transactionLine1 = new LedgerTransactionLine
            {
                LedgerAccount = cashLedgerAccount,
                Amount = 2000,
                IsDebit = true
            };
            transaction.TransactionLines.Add(transactionLine1);

            // Debit != Credit
            Assert.Throws<LedgerTransactionException>(
                () =>
                    _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin"));

            var transactionLine2 = new LedgerTransactionLine
            {
                LedgerAccount = cashLedgerAccount,
                Amount = 2000,
                IsDebit = false
            };
            transaction.TransactionLines.Add(transactionLine2);

            // Multiple lines with the same account
            Assert.Throws<LedgerTransactionException>(
                () =>
                    _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin"));

            transaction.TransactionLines.RemoveAt(1);
            var transactionLine3 = new LedgerTransactionLine
            {
                LedgerAccount = otherCompanyCapitalLedgerAccount,
                Amount = 2000,
                IsDebit = false
            };
            transaction.TransactionLines.Add(transactionLine3);

            // Lines with ledger accounts of different companies
            Assert.Throws<LedgerTransactionException>(
                () =>
                    _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin"));

            transaction.TransactionLines.RemoveAt(1);
            var transactionLine4 = new LedgerTransactionLine
            {
                LedgerAccount = capitalLedgerAccount,
                Amount = 2000,
                IsDebit = false
            };
            transaction.TransactionLines.Add(transactionLine4);

            transaction.PostingDate = DateTime.Now.AddDays(3);

            // Posting date is ahead of current date
            Assert.Throws<LedgerTransactionInvalidPostingDateException>(
                () =>
                    _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin"));

            transaction.PostingDate = DateTime.Now.AddMonths(-1);

            // Posting date is in previous month
            Assert.Throws<LedgerTransactionInvalidPostingDateException>(
                () =>
                    _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin"));

            transaction.PostingDate = DateTime.Now.AddMonths(2);

            // Posting date is in next month
            Assert.Throws<LedgerTransactionInvalidPostingDateException>(
                () =>
                    _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin"));

            transaction.PostingDate = DateTime.Now;
            _fixture.LedgerTransactionService.CreateTransaction(transaction, company.Id, "Admin");
            var savedTransaction = _fixture.LedgerTransactionService.GetSingleById(transaction.Id);
            Assert.Equal(transaction, savedTransaction);
            var areTransactionLinesSame =
                transaction.TransactionLines.All(savedTransaction.TransactionLines.Contains) &&
                transaction.TransactionLines.Count == savedTransaction.TransactionLines.Count;
            Assert.True(areTransactionLinesSame);
        }
        #endregion
    }
}