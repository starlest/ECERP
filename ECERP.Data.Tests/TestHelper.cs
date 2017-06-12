namespace ECERP.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using Core.Domain.Cities;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;
    using Core.Domain.FinancialAccounting;
    using Core.Domain.Products;
    using Core.Domain.Suppliers;

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
        public static SupplierSubscription GetTestSupplierSubscription(this PersistenceTest test)
        {
            return new SupplierSubscription
            {
                Id = 1,
                CompanyId = 1,
                SupplierId = 1,
                LedgerAccountId = 1
            };
        }

        public static City GetTestCity(this PersistenceTest test)
        {
            return new City
            {
                Id = 1,
                Name = "test"
            };
        }

        public static Supplier GetTestSupplier(this PersistenceTest test)
        {
            return new Supplier
            {
                Id = 1,
                Name = "test",
                Address = "test",
                ContactNumber = "test",
                CityId = 1
            };
        }

        public static ProductSubscription GetTestProductSubscription(this PersistenceTest test)
        {
            return new ProductSubscription
            {
                Id = 1,
                ProductId = 1,
                SupplierSubscriptionId = 1
            };
        }

        public static Product GetTestProduct(this PersistenceTest test)
        {
            return new Product
            {
                Id = 1,
                ProductId = "test",
                Name = "test",
                PrimaryUnitName = "test",
                SecondaryUnitName = "test",
                QuantityPerPrimaryUnit = 1,
                QuantityPerSecondaryUnit = 1,
                ProductCategoryId = 2
            };
        }

        public static ProductCategory GetTestProductCategory(this PersistenceTest test)
        {
            return new ProductCategory
            {
                Id = 1,
                Name = "test"
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
                IsHidden = true,
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
                PostingDate = DateTime.Now.Date
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