namespace ECERP.Data.Tests.FinancialAccounting
{
    using Xunit;

    public class LedgerTransactionLinePersistenceTest:PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_ledgerTransactionLine()
        {
            var ledgerTransactionLine = this.GetTestLedgerTransactionLine();
            var fromDb = SaveAndLoadEntity(this.GetTestLedgerTransactionLine());
            Assert.NotNull(fromDb);
            Assert.Equal(ledgerTransactionLine.Id, fromDb.Id);
            Assert.Equal(ledgerTransactionLine.LedgerTransactionId, fromDb.LedgerTransactionId);
            Assert.Equal(ledgerTransactionLine.LedgerAccountId, fromDb.LedgerAccountId);
            Assert.Equal(ledgerTransactionLine.Amount, fromDb.Amount);
            Assert.Equal(ledgerTransactionLine.IsDebit, fromDb.IsDebit);
        }
    }
}
