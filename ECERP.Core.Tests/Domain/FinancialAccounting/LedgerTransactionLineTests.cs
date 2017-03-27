namespace ECERP.Core.Tests.Domain.FinancialAccounting
{
    using Core.Domain.FinancialAccounting;
    using Xunit;

    public class LedgerTransactionLineTests
    {
        [Fact]
        public void Can_create_ledgerTransaction()
        {
            var transactionLine = new LedgerTransactionLine
            {
                LedgerTransactionId = 1,
                LedgerAccountId = 1,
                Amount = 2000,
                IsDebit = true
            };
            Assert.Equal(1, transactionLine.LedgerTransactionId);
            Assert.Equal(1, transactionLine.LedgerAccountId);
            Assert.Equal(2000, transactionLine.Amount);
            Assert.True(transactionLine.IsDebit);
        }
    }
}