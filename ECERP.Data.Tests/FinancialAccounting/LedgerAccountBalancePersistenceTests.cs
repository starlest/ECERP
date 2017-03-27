namespace ECERP.Data.Tests.FinancialAccounting
{
    using Xunit;

    public class LedgerAccountBalancePersistenceTests : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_ledgerAccountBalance()
        {
            var ledgerAccountBalance = this.GetTestLedgerAccountBalance();
            var fromDb = SaveAndLoadEntity(this.GetTestLedgerAccountBalance());
            Assert.NotNull(fromDb);
            Assert.Equal(ledgerAccountBalance.Id, fromDb.Id);
            Assert.Equal(ledgerAccountBalance.BeginningBalance, fromDb.BeginningBalance);
            Assert.Equal(ledgerAccountBalance.Balance1, fromDb.Balance1);
            Assert.Equal(ledgerAccountBalance.Balance2, fromDb.Balance2);
            Assert.Equal(ledgerAccountBalance.Balance3, fromDb.Balance3);
            Assert.Equal(ledgerAccountBalance.Balance4, fromDb.Balance4);
            Assert.Equal(ledgerAccountBalance.Balance5, fromDb.Balance5);
            Assert.Equal(ledgerAccountBalance.Balance6, fromDb.Balance6);
            Assert.Equal(ledgerAccountBalance.Balance7, fromDb.Balance7);
            Assert.Equal(ledgerAccountBalance.Balance8, fromDb.Balance8);
            Assert.Equal(ledgerAccountBalance.Balance9, fromDb.Balance9);
            Assert.Equal(ledgerAccountBalance.Balance10, fromDb.Balance10);
            Assert.Equal(ledgerAccountBalance.Balance11, fromDb.Balance11);
            Assert.Equal(ledgerAccountBalance.Balance12, fromDb.Balance12);
        }
    }
}