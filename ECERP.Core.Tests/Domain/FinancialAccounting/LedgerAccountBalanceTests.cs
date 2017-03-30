namespace ECERP.Core.Tests.Domain.FinancialAccounting
{
    using System.Collections.Generic;
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

        [Fact]
        public void Can_calculate_ledger_transaction_lines_total()
        {
            var transactionLines = new List<LedgerTransactionLine>
            {
                new LedgerTransactionLine
                {
                    LedgerTransactionId = 1,
                    LedgerAccount = new LedgerAccount
                    {
                        Type = LedgerAccountType.Asset
                    },
                    Amount = 2000,
                    IsDebit = true
                },
                new LedgerTransactionLine
                {
                    LedgerTransactionId = 1,
                    LedgerAccount = new LedgerAccount
                    {
                        Type = LedgerAccountType.Asset
                    },
                    Amount = 2000,
                    IsDebit = false
                },
                new LedgerTransactionLine
                {
                    LedgerTransactionId = 1,
                    LedgerAccount = new LedgerAccount
                    {
                        Type = LedgerAccountType.Liability
                    },
                    Amount = 2000,
                    IsDebit = true
                },
                new LedgerTransactionLine
                {
                    LedgerTransactionId = 1,
                    LedgerAccount = new LedgerAccount
                    {
                        Type = LedgerAccountType.Equity
                    },
                    Amount = 2000,
                    IsDebit = false
                },
            };
            var total = LedgerAccountBalanceExtensions.CalculateLedgerTransactionLinesTotal(transactionLines);
            Assert.Equal(0, total);
        }
    }
}