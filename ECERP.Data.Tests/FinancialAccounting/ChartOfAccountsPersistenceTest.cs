namespace ECERP.Data.Tests.FinancialAccounting
{
    using Xunit;

    public class ChartOfAccountsPersistenceTest : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_chartOfAccounts()
        {
            var coa = this.GetTestChartOfAccounts();
            var fromDb = SaveAndLoadEntity(this.GetTestChartOfAccounts());
            Assert.NotNull(fromDb);
            Assert.Equal(coa.Id, fromDb.Id);
            Assert.Equal(coa.CurrentLedgerPeriodStartDate, fromDb.CurrentLedgerPeriodStartDate);
        }
    }
}