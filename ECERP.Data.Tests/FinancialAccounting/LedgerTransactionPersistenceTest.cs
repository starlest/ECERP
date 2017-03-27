namespace ECERP.Data.Tests.FinancialAccounting
{
    using Xunit;

    public class LedgerTransactionPersistenceTest : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_ledgerTransaction()
        {
            var ledgerTransaction = this.GetTestLedgerTransaction();
            var fromDb = SaveAndLoadEntity(this.GetTestLedgerTransaction());
            Assert.NotNull(fromDb);
            Assert.Equal(ledgerTransaction.Id, fromDb.Id);
            Assert.Equal(ledgerTransaction.Documentation, fromDb.Documentation);
            Assert.Equal(ledgerTransaction.Description, fromDb.Description);
            Assert.Equal(ledgerTransaction.PostingDate, fromDb.PostingDate);
        }
    }
}