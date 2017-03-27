namespace ECERP.Services.Tests.FinancialAccounting
{
    using System;
    using Core;
    using Core.Domain.FinancialAccounting;
    using Data.Abstract;
    using ECERP.Services.Configuration;
    using ECERP.Services.FinancialAccounting;
    using Moq;
    using Xunit;

    public class LedgerTransactionServiceTests : ServiceTests
    {
        private readonly ILedgerTransactionService _ledgerTransactionService;
        private readonly Mock<IRepository> _mockRepo;

        public LedgerTransactionServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.GetById<LedgerTransaction>(It.IsAny<object>()))
                .Returns(this.GetTestLedgerTransaction);
            _ledgerTransactionService = new LedgerTransactionService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_ledgerTransaction_by_id()
        {
            var testTransaction = this.GetTestLedgerTransaction();
            var result = _ledgerTransactionService.GetLedgerTransactionById(testTransaction.Id);
            Assert.Equal(testTransaction, result);
            Assert.True(CommonHelper.ListsEqual(testTransaction.LedgerTransactionLines, result.LedgerTransactionLines));
        }

        [Fact]
        public void Can_insert_ledgerTransaction()
        {
            var testTransactionWithNoLines = this.GetTestLedgerTransactionWithNoLines();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransactionWithNoLines));

            var testTransactionWithDifferentCOAs = this.GetTestLedgerTransactionWithDifferentCOAs();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransactionWithDifferentCOAs));

            var testTransactionWithDifferentPostingPeriod = this.GetTestLedgerTransactionWithDifferentPostingPeriod();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransactionWithDifferentPostingPeriod));

            var testTransactionWithDifferentTotalDebitAndTotalCreditAmounts =
                this.GetTestLedgerTransactionWithDifferentTotalDebitAndTotalCreditAmounts();
            Assert.Throws<ArgumentException>(
                () =>
                    _ledgerTransactionService.InsertLedgerTransaction(
                        testTransactionWithDifferentTotalDebitAndTotalCreditAmounts));

            var testTransactionWithDuplicateLedgerAccounts = this.GetTestLedgerTransactionWithDuplicateLedgerAccounts();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransactionWithDuplicateLedgerAccounts));

            var testTransaction = this.GetTestLedgerTransaction();
            _ledgerTransactionService.InsertLedgerTransaction(testTransaction);
            _mockRepo.Verify(x => x.Create(testTransaction), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}