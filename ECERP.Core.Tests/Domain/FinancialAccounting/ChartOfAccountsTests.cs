namespace ECERP.Core.Tests.Domain.FinancialAccounting
{
    using System;
    using System.Linq;
    using Core.Domain.FinancialAccounting;
    using Xunit;
    
    public class ChartOfAccountsTests
    {
        [Fact]
        public void Can_create_chartOfAccounts()
        {
            var coa = new ChartOfAccounts { CompanyId = 1 };
            Assert.Equal(1, coa.CompanyId);
            Assert.Equal(DateTime.Now.Year, coa.CurrentLedgerPeriodStartDate.Year);
            Assert.Equal(DateTime.Now.Month, coa.CurrentLedgerPeriodStartDate.Month);
            Assert.NotNull(coa.LedgerAccounts);
        }

        [Fact]
        public void Can_add_ledgerAccount()
        {
            var coa = new ChartOfAccounts { CompanyId = 1 };
            var ledgerAccount = new LedgerAccount { Id = 1 };
            coa.LedgerAccounts.Add(ledgerAccount);
            Assert.Equal(1, coa.LedgerAccounts.Count);
            Assert.Equal(1, coa.LedgerAccounts.First().Id);
        }

        [Fact]
        public void Can_advance_ledger_period()
        {
            var coa = new ChartOfAccounts { CompanyId = 1 };
            coa.AdvanceLedgerPeriod();
            Assert.Equal(DateTime.Now.Year, coa.CurrentLedgerPeriodStartDate.Year);
            Assert.Equal(DateTime.Now.Month + 1, coa.CurrentLedgerPeriodStartDate.Month);
        }

        [Fact]
        public void Can_regress_ledger_period()
        {
            var coa = new ChartOfAccounts { CompanyId = 1 };
            coa.RegressLedgerPeriod();
            Assert.Equal(DateTime.Now.Year, coa.CurrentLedgerPeriodStartDate.Year);
            Assert.Equal(DateTime.Now.Month - 1, coa.CurrentLedgerPeriodStartDate.Month);
        }
    }
}