namespace ECERP.Core.Tests.Domain.FinancialAccounting
{
    using Core.Domain.FinancialAccounting;
    using Xunit;

    public class LedgerAccountTests
    {
        [Fact]
        public void Can_create_ledgerAccount()
        {
            var ledgerAccount = new LedgerAccount
            {
                AccountNumber = 00000,
                Name = "Test",
                Description = "Test",
                Type = LedgerAccountType.Asset,
                Group = LedgerAccountGroup.CashAndBank,
                IsActive = true,
                IsDefault = true
            };
            Assert.Equal(00000, ledgerAccount.AccountNumber);
            Assert.Equal("Test", ledgerAccount.Name);
            Assert.Equal("Test", ledgerAccount.Description);
            Assert.Equal(LedgerAccountType.Asset, ledgerAccount.Type);
            Assert.Equal(LedgerAccountGroup.CashAndBank, ledgerAccount.Group);
            Assert.True(ledgerAccount.IsActive);
            Assert.True(ledgerAccount.IsDefault);
        }
    }
}