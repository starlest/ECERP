namespace ECERP.Core.Tests.Domain.FinancialAccounting
{
    using System;
    using System.Linq;
    using Core.Domain.FinancialAccounting;
    using Xunit;

    public class LedgerTransactionTests
    {
        [Fact]
        public void Can_create_ledgerTransaction()
        {
            var postingDate = DateTime.UtcNow;
            var transaction = new LedgerTransaction
            {
                Documentation = "Test",
                Description = "Test",
                PostingDate = postingDate
            };
            Assert.Equal("Test", transaction.Documentation);
            Assert.Equal("Test", transaction.Description);
            Assert.Equal(postingDate, transaction.PostingDate);
            Assert.NotNull(transaction.LedgerTransactionLines);
        }

        [Fact]
        public void Can_add_ledgerTransactionLine()
        {
            var transaction = new LedgerTransaction
            {
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow
            };
            var transactionLine1 = new LedgerTransactionLine { Id = 1 };
            transaction.LedgerTransactionLines.Add(transactionLine1);
            Assert.Equal(1, transaction.LedgerTransactionLines.Count);
            Assert.Equal(1, transaction.LedgerTransactionLines.First().Id);
        }

        [Fact]
        public void Can_calculate_ledgerTransaction_totalDebitAmount()
        {
            var transaction = new LedgerTransaction
            {
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow
            };
            var transactionLine1 = new LedgerTransactionLine { Id = 1, Amount = 2000, IsDebit = true };
            var transactionLine2 = new LedgerTransactionLine { Id = 2, Amount = 2000, IsDebit = false };
            var transactionLine3 = new LedgerTransactionLine { Id = 3, Amount = 1000, IsDebit = true };
            transaction.LedgerTransactionLines.Add(transactionLine1);
            transaction.LedgerTransactionLines.Add(transactionLine2);
            transaction.LedgerTransactionLines.Add(transactionLine3);
            var totalDebit = transaction.GetTotalDebitAmount();
            Assert.Equal(3000, totalDebit);
        }

        [Fact]
        public void Can_calculate_ledgerTransaction_totalCreditAmount()
        {
            var transaction = new LedgerTransaction
            {
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow
            };
            var transactionLine1 = new LedgerTransactionLine { Id = 1, Amount = 2000, IsDebit = true };
            var transactionLine2 = new LedgerTransactionLine { Id = 2, Amount = 2000, IsDebit = false };
            var transactionLine3 = new LedgerTransactionLine { Id = 3, Amount = 1000, IsDebit = true };
            transaction.LedgerTransactionLines.Add(transactionLine1);
            transaction.LedgerTransactionLines.Add(transactionLine2);
            transaction.LedgerTransactionLines.Add(transactionLine3);
            var totalCredit = transaction.GetTotalCreditAmount();
            Assert.Equal(2000, totalCredit);
        }

        [Fact]
        public void Can_check_whether_there_are_duplicate_ledgerAccounts()
        {
            var transaction = new LedgerTransaction
            {
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow
            };
            var transactionLine1 = new LedgerTransactionLine
            {
                Id = 1,
                LedgerAccountId = 1,
                Amount = 2000,
                IsDebit = true
            };
            var transactionLine2 = new LedgerTransactionLine
            {
                Id = 2,
                LedgerAccountId = 1,
                Amount = 2000,
                IsDebit = false
            };
            var transactionLine3 = new LedgerTransactionLine
            {
                Id = 3,
                LedgerAccountId = 3,
                Amount = 1000,
                IsDebit = true
            };
            transaction.LedgerTransactionLines.Add(transactionLine1);
            transaction.LedgerTransactionLines.Add(transactionLine2);
            transaction.LedgerTransactionLines.Add(transactionLine3);
            var areThereDuplicateLedgerAccounts = transaction.AreThereDuplicateAccounts();
            Assert.True(areThereDuplicateLedgerAccounts);
        }
    }
}