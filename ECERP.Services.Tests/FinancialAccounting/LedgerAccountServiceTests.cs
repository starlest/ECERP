namespace ECERP.Services.Tests.FinancialAccounting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            _mockRepo.Setup(
                    x =>
                        x.Get(It.IsAny<Expression<Func<LedgerAccount, bool>>>(), null, null, null,
                            It.IsAny<Expression<Func<LedgerAccount, object>>[]>()))
                .Returns(new List<LedgerAccount>());

            _mockRepo.Setup(
                    x =>
                        x.Get(It.IsAny<Expression<Func<LedgerAccount, bool>>>(), null, 0, int.MaxValue,
                            It.IsAny<Expression<Func<LedgerAccount, object>>[]>()))
                .Returns(this.GetTestLedgerAccounts());

            _mockRepo.Setup(
                    x =>
                        x.Get(It.IsAny<Expression<Func<LedgerAccount, bool>>>(), null, 0, 5,
                            It.IsAny<Expression<Func<LedgerAccount, object>>[]>()))
                .Returns(this.GetTestLedgerAccounts().Take(5));

            _mockRepo.Setup(x => x.GetById(It.IsAny<object>(), It.IsAny<Expression<Func<LedgerAccount, object>>[]>()))
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
        public void Can_get_ledgerAccounts()
        {
            var testLedgerAccounts = this.GetTestLedgerAccounts();
            var results = _ledgerAccountService.GetLedgerAccounts();
            Assert.True(CommonHelper.ListsEqual(testLedgerAccounts, results));
        }

        [Fact]
        public void Can_get_paged_ledgerAccounts()
        {
            var results = _ledgerAccountService.GetLedgerAccounts(pageIndex: 0, pageSize: 5);
            Assert.Equal(5, results.Count);
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

            var incompatibleGroupTypeTestLedgerAccount = this.GetIncompatibleGroupTypeTestLedgerAccount();
            Assert.Throws<ArgumentException>(
                () => _ledgerAccountService.InsertLedgerAccount(incompatibleGroupTypeTestLedgerAccount));
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

        [Fact]
        public void Can_generate_new_account_number()
        {
            var accountNumber = _ledgerAccountService.GenerateNewAccountNumber(LedgerAccountGroup.CashAndBank);
            Assert.Equal(1010001, accountNumber);
            accountNumber = _ledgerAccountService.GenerateNewAccountNumber(LedgerAccountGroup.TreasuryStock);
            Assert.Equal(3040001, accountNumber);
        }
    }
}