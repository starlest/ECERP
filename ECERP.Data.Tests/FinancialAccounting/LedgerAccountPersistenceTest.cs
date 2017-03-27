namespace ECERP.Data.Tests.FinancialAccounting
{
    using Xunit;

    public class LedgerAccountPersistenceTest : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_chartOfAccounts()
        {
            var ledgerAccount = this.GetTestLedgerAccount();
            var fromDb = SaveAndLoadEntity(this.GetTestLedgerAccount());
            Assert.NotNull(fromDb);
            Assert.Equal(ledgerAccount.Id, fromDb.Id);
            Assert.Equal(ledgerAccount.AccountNumber, fromDb.AccountNumber);
            Assert.Equal(ledgerAccount.Name, fromDb.Name);
            Assert.Equal(ledgerAccount.Description, fromDb.Description);
            Assert.Equal(ledgerAccount.Type, fromDb.Type);
            Assert.Equal(ledgerAccount.Group, fromDb.Group);
            Assert.Equal(ledgerAccount.IsActive, fromDb.IsActive);
            Assert.Equal(ledgerAccount.IsDefault, fromDb.IsDefault);
            Assert.Equal(ledgerAccount.ChartOfAccountsId, fromDb.ChartOfAccountsId);
        }
    }
}