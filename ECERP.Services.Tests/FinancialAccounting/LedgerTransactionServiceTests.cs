namespace ECERP.Services.Tests.FinancialAccounting
{
    using System;
    using System.Linq.Expressions;
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
            _mockRepo.Setup(
                    x => x.GetById<LedgerTransaction>(It.IsAny<object>()))
                .Returns(this.GetTestLedgerTransaction);
            _mockRepo.Setup(
                    x => x.GetById(It.IsAny<object>(), It.IsAny<Expression<Func<LedgerTransaction, object>>[]>()))
                .Returns(this.GetTestLedgerTransaction);
            _mockRepo.Setup(x => x.GetById<ChartOfAccounts>(1))
                .Returns(this.GetTestChartOfAccounts(1));
            _mockRepo.Setup(x => x.GetById<ChartOfAccounts>(2))
                .Returns(this.GetTestChartOfAccounts(2));
            _mockRepo.SetupSequence(x => x.GetById<LedgerAccount>(It.IsAny<object>()))
                .Returns(this.GetTestLedgerAccounts()[0])
                .Returns(this.GetTestLedgerAccounts()[1])
                .Returns(this.GetTestLedgerAccounts()[2])
                .Returns(this.GetTestLedgerAccounts()[3])
                .Returns(this.GetTestLedgerAccounts()[4])
                .Returns(this.GetTestLedgerAccounts()[5])
                .Returns(this.GetTestLedgerAccounts()[6])
                .Returns(this.GetTestLedgerAccounts()[7])
                .Returns(this.GetTestLedgerAccounts()[8])
                .Returns(this.GetTestLedgerAccounts()[9])
                .Returns(this.GetTestLedgerAccounts()[10])
                .Returns(this.GetTestLedgerAccounts()[11])
                .Returns(this.GetTestLedgerAccounts()[12])
                .Returns(this.GetTestLedgerAccounts()[13])
                .Returns(this.GetTestLedgerAccounts()[14])
                .Returns(this.GetTestLedgerAccounts()[15])
                .Returns(this.GetTestLedgerAccounts()[16])
                .Returns(this.GetTestLedgerAccounts()[17]);
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
        public void Cannot_insert_ledgerTransaction_with_no_lines()
        {
            var testTransaction = this.GetTestLedgerTransactionWithNoLines();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransaction));
        }

        [Fact]
        public void Cannot_insert_ledgerTransaction_with_different_coas()
        {
            var testTransaction = this.GetTestLedgerTransactionWithDifferentCOAs();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransaction));
        }

        [Fact]
        public void Cannot_insert_ledgerTransaction_with_different_posting_period()
        {
            var testTransaction = this.GetTestLedgerTransactionWithDifferentPostingPeriod();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransaction));
        }

        [Fact]
        public void Cannot_insert_ledgerTransaction_with_different_total_debit_credit_amounts()
        {
            var testTransaction =
                this.GetTestLedgerTransactionWithDifferentTotalDebitAndTotalCreditAmounts();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransaction));
        }

        [Fact]
        public void Cannot_insert_ledgerTransaction_with_duplicate_ledger_accounts()
        {
            var testTransaction = this.GetTestLedgerTransactionWithDuplicateLedgerAccounts();
            Assert.Throws<ArgumentException>(
                () => _ledgerTransactionService.InsertLedgerTransaction(testTransaction));
        }

        [Fact]
        public void Can_insert_ledgerTransaction()
        {
            var testTransaction = this.GetTestLedgerTransaction();
            _ledgerTransactionService.InsertLedgerTransaction(testTransaction);
            _mockRepo.Verify(x => x.Create(testTransaction), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void Can_delete_ledgerTransaction()
        {
            var testTransaction = this.GetTestLedgerTransaction();
            _ledgerTransactionService.DeleteLedgerTransaction(testTransaction.Id);
            _mockRepo.Verify(x => x.Delete(testTransaction), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}