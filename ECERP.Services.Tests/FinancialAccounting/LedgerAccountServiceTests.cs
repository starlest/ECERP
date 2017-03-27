namespace ECERP.Services.Tests.FinancialAccounting
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.FinancialAccounting;
    using Data.Abstract;
    using ECERP.Services.FinancialAccounting;
    using Moq;
    using Xunit;

    public class LedgerAccountServiceTests : ServiceTests
    {
        private readonly ILedgerAccountService _ledgerAccountService;
        private readonly Mock<IRepository> _mockRepo;

        public LedgerAccountServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.GetById<ChartOfAccounts>(It.IsAny<object>()))
                .Returns(this.GetTestChartOfAccounts);
            _mockRepo.Setup(x => x.GetById<LedgerAccount>(It.IsAny<object>()))
                .Returns(this.GetTestLedgerAccount);
            _mockRepo.Setup(x => x.GetOne(It.IsAny<Expression<Func<LedgerAccount, bool>>>()))
                .Returns(this.GetTestLedgerAccount());
            _mockRepo.SetupSequence(x => x.GetOne(It.IsAny<Expression<Func<LedgerAccountBalance, bool>>>()))
                .Returns(null)
                .Returns(this.GetTestLedgerAccountBalance())
                .Returns(this.GetTestLedgerAccountBalance());
            _ledgerAccountService = new LedgerAccountService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_all_ledgerAccounts_by_COAId()
        {
            var testCOA = this.GetTestChartOfAccounts();
            var results = _ledgerAccountService.GetAllLedgerAccountsByCOAId(testCOA.Id);
            Assert.Equal(16, results.Count);
            Assert.True(CommonHelper.ListsEqual(testCOA.LedgerAccounts, results));
        }

        [Fact]
        public void Can_get_ledgerAccount_by_id()
        {
            var testLedgerAccount = this.GetTestLedgerAccount();
            var result = _ledgerAccountService.GetLedgerAccountById(testLedgerAccount.Id);
            Assert.Equal(testLedgerAccount, result);
        }

        [Fact]
        public void Can_get_ledgerAccount_by_name()
        {
            var testLedgerAccount = this.GetTestLedgerAccount();
            var result = _ledgerAccountService.GetLedgerAccountByName(testLedgerAccount.ChartOfAccountsId,
                testLedgerAccount.Name);
            Assert.Equal(testLedgerAccount, result);
        }

        [Fact]
        public void Can_insert_ledgerAccount()
        {
            var testLedgerAccount = this.GetTestLedgerAccount();
            _ledgerAccountService.InsertLedgerAccount(testLedgerAccount);
            _mockRepo.Verify(x => x.Create(testLedgerAccount), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void Can_get_period_ledgerAccount_balance()
        {
            var testLedgerAccount = this.GetTestLedgerAccount();
            var balance = _ledgerAccountService.GetPeriodLedgerAccountBalance(testLedgerAccount, 2000, 12);
            Assert.Equal(0, balance);
            balance = _ledgerAccountService.GetPeriodLedgerAccountBalance(testLedgerAccount, 2017, 1);
            Assert.Equal(1000, balance);
            balance = _ledgerAccountService.GetPeriodLedgerAccountBalance(testLedgerAccount, 2017, 2);
            Assert.Equal(2000, balance);
        }
    }
}