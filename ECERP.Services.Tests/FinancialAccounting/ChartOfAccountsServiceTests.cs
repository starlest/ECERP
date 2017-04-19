namespace ECERP.Services.Tests.FinancialAccounting
{
    using Core.Domain.FinancialAccounting;
    using Data.Abstract;
    using Services.FinancialAccounting;
    using Moq;
    using Xunit;

    public class ChartOfAccountsServiceTests : ServiceTests
    {
        private readonly IChartOfAccountsService _chartOfAccountsService;
        private readonly Mock<IRepository> _mockRepo;

        public ChartOfAccountsServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.GetById<ChartOfAccounts>(It.IsAny<object>())).Returns(this.GetTestChartOfAccounts(1));
            _chartOfAccountsService = new ChartOfAccountsService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_coa_by_id()
        {
            var testCoa = this.GetTestChartOfAccounts(1);
            var result = _chartOfAccountsService.GetChartOfAccountsById(testCoa.Id);
            Assert.Equal(testCoa, result);
        }

        [Fact]
        public void Can_regress_ledger_period()
        {
            var testCoa = this.GetTestChartOfAccounts(1);
            _chartOfAccountsService.RegressLedgerPeriod(testCoa.Id);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void Can_close_ledger_period()
        {
            var testCoa = this.GetTestChartOfAccounts(1);
            _chartOfAccountsService.CloseLedgerPeriod(testCoa.Id);
            _mockRepo.Verify(x => x.Update(It.IsAny<LedgerAccountBalance>()), Times.Exactly(3));
            _mockRepo.Verify(x => x.Update(It.IsAny<ChartOfAccounts>()), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}