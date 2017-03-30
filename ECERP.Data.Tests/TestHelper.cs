﻿namespace ECERP.Data.Tests
{
    using System;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;
    using Core.Domain.FinancialAccounting;

    public static class TestHelper
    {
        public static Company GetTestCompany(this PersistenceTest test)
        {
            return new Company
            {
                Id = 1,
                Name = "Test Company"
            };
        }

        public static CompanySetting GetTestCompanySetting(this PersistenceTest test)
        {
            return new CompanySetting
            {
                Id = 1,
                Key = "TestSetting",
                Value = "TestSettingValue",
                CompanyId = test.GetTestCompany().Id
            };
        }

        public static ChartOfAccounts GetTestChartOfAccounts(this PersistenceTest test)
        {
            return new ChartOfAccounts { Id = 1 };
        }

        public static LedgerAccount GetTestLedgerAccount(this PersistenceTest test)
        {
            return new LedgerAccount
            {
                Id = 1,
                AccountNumber = 00000,
                Name = "Test",
                Description = "Test",
                Type = LedgerAccountType.Asset,
                Group = LedgerAccountGroup.CashAndBank,
                IsActive = true,
                IsDefault = true,
                ChartOfAccountsId = test.GetTestChartOfAccounts().Id
            };
        }

        public static LedgerAccountBalance GetTestLedgerAccountBalance(this PersistenceTest test)
        {
            return new LedgerAccountBalance
            {
                Id = 1,
                BeginningBalance = 1000,
                Balance3 = 2000
            };
        }

        public static LedgerTransaction GetTestLedgerTransaction(this PersistenceTest test)
        {
            return new LedgerTransaction
            {
                Id = 1,
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow
            };
        }

        public static LedgerTransactionLine GetTestLedgerTransactionLine(this PersistenceTest test)
        {
            return new LedgerTransactionLine
            {
                Id = 1,
                LedgerTransactionId = 1,
                LedgerAccountId = 1,
                Amount = 2000,
                IsDebit = true
            };
        }
    }
}