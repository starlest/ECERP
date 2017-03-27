namespace ECERP.Core.Tests.Domain.FinancialAccounting
{
    using Core.Domain.FinancialAccounting;
    using Xunit;

    public class LedgerAccountBalanceTests
    {
        [Fact]
        public void Can_create_ledgerAccountBalance()
        {
            var ledgerAccountBalance = new LedgerAccountBalance
            {
                LedgerAccountId = 1,
                BeginningBalance = 0,
                Balance3 = 1000
            };
            Assert.Equal(1, ledgerAccountBalance.LedgerAccountId);
            Assert.Equal(0, ledgerAccountBalance.BeginningBalance);
            Assert.Equal(1000, ledgerAccountBalance.Balance3);
        }

        [Fact]
        public void Can_get_monthBalance()
        {
            var ledgerAccountBalance = new LedgerAccountBalance
            {
                LedgerAccountId = 1,
                BeginningBalance = 0,
                Balance3 = 1000
            };
            Assert.Equal(0, ledgerAccountBalance.GetMonthBalance(1));
            Assert.Equal(1000, ledgerAccountBalance.GetMonthBalance(3));
        }

        [Fact]
        public void Can_set_monthBalance()
        {
            var ledgerAccountBalance = new LedgerAccountBalance
            {
                LedgerAccountId = 1,
                BeginningBalance = 0,
                Balance3 = 1000
            };
            ledgerAccountBalance.SetMonthBalance(0, 3000);
            Assert.Equal(3000, ledgerAccountBalance.GetMonthBalance(0));
        }
    }
}