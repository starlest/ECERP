﻿namespace ECERP.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;
    using Core.Domain.FinancialAccounting;

    public static class TestHelper
    {
        public static IList<Company> GetTestCompanies(this ServiceTests tests)
        {
            return new List<Company>
            {
                new Company { Name = "test1", Id = 1 },
                new Company { Name = "test2", Id = 2 },
                new Company { Name = "test3", Id = 3 },
            };
        }

        public static Company GetTestCompany(this ServiceTests tests)
        {
            return tests.GetTestCompanies().First();
        }

        public static IList<CompanySetting> GetTestCompanySettings(this ServiceTests tests)
        {
            return new List<CompanySetting>
            {
                new CompanySetting
                {
                    Id = 1,
                    Key = "setting1",
                    Value = "Value1",
                    CompanyId = tests.GetTestCompany().Id
                },
                new CompanySetting
                {
                    Id = 2,
                    Key = "setting2",
                    Value = "Value2",
                    CompanyId = tests.GetTestCompany().Id
                },
                new CompanySetting
                {
                    Id = 3,
                    Key = "setting3",
                    Value = "Value3",
                    CompanyId = tests.GetTestCompany().Id
                },
                new CompanySetting
                {
                    Id = 4,
                    Key = "setting4",
                    Value = "Value4",
                    CompanyId = tests.GetTestCompany().Id
                }
            };
        }

        public static CompanySetting GetTestCompanySetting(this ServiceTests tests)
        {
            return tests.GetTestCompanySettings().First();
        }

        public static ChartOfAccounts GetTestChartOfAccounts(this ServiceTests tests, int id)
        {
            return new ChartOfAccounts
            {
                Id = id,
                CompanyId = tests.GetTestCompany().Id,
                LedgerAccounts = tests.GetTestLedgerAccounts()
            };
        }

        public static IList<LedgerAccount> GetTestLedgerAccounts(this ServiceTests tests)
        {
            return new List<LedgerAccount>
            {
                new LedgerAccount
                {
                    Id = 1,
                    AccountNumber = 1010001,
                    Name = "Cash",
                    Description =
                        "Checking account balance, currency, coins, checks received from customers but not yet deposited.",
                    Type = LedgerAccountType.Asset,
                    Group = LedgerAccountGroup.CashAndBank,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 2,
                    AccountNumber = 2,
                    Name = "Inventory",
                    Description =
                        "Cost of inventory purchased but has not yet been sold.",
                    Type = LedgerAccountType.Asset,
                    Group = LedgerAccountGroup.Inventory,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = true,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 3,
                    AccountNumber = 3,
                    Name = "Sales Revenue",
                    Description =
                        "Amounts earned from providing goods to clients, either for cash or on credit during the accounting period.",
                    Type = LedgerAccountType.Revenue,
                    Group = LedgerAccountGroup.SalesRevenue,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = true,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 4,
                    AccountNumber = 4,
                    Name = "Cost Of Goods Sold",
                    Description =
                        "The cost of the goods sold by the company to clients, either for cash or on credit during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.CostOfGoodsSold,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = true,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 5,
                    AccountNumber = 5,
                    Name = "Comissions Expense",
                    Description = "The amount of commissions paid to salesmen during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 6,
                    AccountNumber = 6,
                    Name = "Freight Expense",
                    Description = "Cost for delivering goods to customers during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 7,
                    AccountNumber = 7,
                    Name = "Cost of Sales-Freight",
                    Description = "Cost of bringing in goods from suppliers.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 8,
                    AccountNumber = 8,
                    Name = "Salaries Expense (Selling)",
                    Description = "The amount of salaries paid to sales employees during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 9,
                    AccountNumber = 9,
                    Name = "Other Expense (Selling)",
                    Description = "Other selling expenses for the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.SellingExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 10,
                    AccountNumber = 10,
                    Name = "Salaries Expense (Administrative)",
                    Description =
                        "The amount of salaries paid to administrative employees during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 11,
                    AccountNumber = 11,
                    Name = "Office Supplies Expense",
                    Description = "The cost of supplies purchased for usage in the office during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 12,
                    AccountNumber = 12,
                    Name = "Office Equipment Expense",
                    Description =
                        "The cost of equipment purchased for usage in the office during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 13,
                    AccountNumber = 13,
                    Name = "Utilities Expense",
                    Description =
                        "Costs for electricity, heat, water, and sewer that were used during the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 14,
                    AccountNumber = 14,
                    Name = "Telephone Expense",
                    Description =
                        "Cost of telephone used during the current accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 15,
                    AccountNumber = 15,
                    Name = "Other Expense (Administrative)",
                    Description =
                        "Other administrative expense for the accounting period.",
                    Type = LedgerAccountType.Expense,
                    Group = LedgerAccountGroup.AdministrativeExpenses,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = false,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 16,
                    AccountNumber = 16,
                    Name = "Capital",
                    Description =
                        "Amount the owner invested in the company not withdrawn by the owner.",
                    Type = LedgerAccountType.Equity,
                    Group = LedgerAccountGroup.CommonStock,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = true,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 17,
                    AccountNumber = 17,
                    Name = "Sales Returns and Allowances",
                    Description =
                        "Amounts of merchandise returned by customers and allowances granted to customers during the period",
                    Type = LedgerAccountType.ContraRevenue,
                    Group = LedgerAccountGroup.SalesReturnsAndAllowances,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = true,
                    ChartOfAccountsId = 1
                },
                new LedgerAccount
                {
                    Id = 18,
                    AccountNumber = -5010001,
                    Name = "Purchase Returns and Allowances",
                    Description =
                        "Amounts of merchandise returned to suppliers and allowances granted by suppliers during the period",
                    Type = LedgerAccountType.ContraExpense,
                    Group = LedgerAccountGroup.PurchaseReturnsAndAllowances,
                    IsActive = true,
                    IsDefault = true,
                    IsHidden = true,
                    ChartOfAccountsId = 1
                }
            };
        }

        public static LedgerAccount GetTestLedgerAccount(this ServiceTests tests)
        {
            return tests.GetTestLedgerAccounts().First();
        }

        public static LedgerAccount GetIncompatibleGroupTypeTestLedgerAccount(this ServiceTests tests)
        {
            return new LedgerAccount
            {
                Id = 18,
                AccountNumber = 18,
                Name = "Purchase Returns and Allowances",
                Description =
                    "Amounts of merchandise returned to suppliers and allowances granted by suppliers during the period",
                Type = LedgerAccountType.Asset,
                Group = LedgerAccountGroup.PurchaseReturnsAndAllowances,
                IsActive = true,
                IsDefault = true,
                IsHidden = true,
                ChartOfAccountsId = 1
            };
        }

        public static LedgerAccountBalance GetTestLedgerAccountBalance(this ServiceTests tests)
        {
            return new LedgerAccountBalance
            {
                Id = 1,
                LedgerAccountId = 1,
                Year = 2017,
                BeginningBalance = 1000,
                Balance1 = 1000,
                Balance2 = 2000
            };
        }

        public static LedgerTransaction GetTestLedgerTransaction(this ServiceTests tests)
        {
            return new LedgerTransaction
            {
                Id = 1,
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow,
                LedgerTransactionLines = tests.GetTestLedgerTransactionLines(),
                ChartOfAccountsId = 1
            };
        }

        public static IList<LedgerTransactionLine> GetTestLedgerTransactionLines(this ServiceTests tests)
        {
            return new List<LedgerTransactionLine>
            {
                new LedgerTransactionLine
                {
                    Id = 1,
                    LedgerTransactionId = 1,
                    LedgerAccountId = tests.GetTestLedgerAccounts()[0].Id,
                    LedgerAccount = tests.GetTestLedgerAccounts()[0],
                    Amount = 2000,
                    IsDebit = true
                },
                new LedgerTransactionLine
                {
                    Id = 2,
                    LedgerTransactionId = 1,
                    LedgerAccountId = tests.GetTestLedgerAccounts()[1].Id,
                    LedgerAccount = tests.GetTestLedgerAccounts()[1],
                    Amount = 2000,
                    IsDebit = true
                },
                new LedgerTransactionLine
                {
                    Id = 3,
                    LedgerTransactionId = 1,
                    LedgerAccountId = tests.GetTestLedgerAccounts()[2].Id,
                    LedgerAccount = tests.GetTestLedgerAccounts()[2],
                    Amount = 4000,
                    IsDebit = false
                }
            };
        }

        public static LedgerTransaction GetTestLedgerTransactionWithNoLines(this ServiceTests tests)
        {
            return new LedgerTransaction
            {
                Id = 1,
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow,
                ChartOfAccountsId = 1
            };
        }

        public static LedgerTransaction GetTestLedgerTransactionWithDifferentCOAs(this ServiceTests tests)
        {
            return new LedgerTransaction
            {
                Id = 1,
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow,
                LedgerTransactionLines = tests.GetTestLedgerTransactionLines(),
                ChartOfAccountsId = 2
            };
        }

        public static LedgerTransaction GetTestLedgerTransactionWithDifferentTotalDebitAndTotalCreditAmounts(
            this ServiceTests tests)
        {
            var transactionLines = new List<LedgerTransactionLine>();
            transactionLines.AddRange(tests.GetTestLedgerTransactionLines());
            transactionLines.Add(new LedgerTransactionLine
            {
                Id = 3,
                LedgerTransactionId = 1,
                LedgerAccountId = tests.GetTestLedgerAccounts()[3].Id,
                LedgerAccount = tests.GetTestLedgerAccounts()[3],
                Amount = 4000,
                IsDebit = false
            });
            return new LedgerTransaction
            {
                Id = 1,
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow,
                LedgerTransactionLines = transactionLines,
                ChartOfAccountsId = 1
            };
        }

        public static LedgerTransaction GetTestLedgerTransactionWithDifferentPostingPeriod(this ServiceTests tests)
        {
            return new LedgerTransaction
            {
                Id = 1,
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow.AddMonths(-1),
                LedgerTransactionLines = tests.GetTestLedgerTransactionLines(),
                ChartOfAccountsId = 1
            };
        }

        public static LedgerTransaction GetTestLedgerTransactionWithDuplicateLedgerAccounts(this ServiceTests tests)
        {
            var transactionLines = new List<LedgerTransactionLine>();
            transactionLines.AddRange(tests.GetTestLedgerTransactionLines());
            transactionLines.Add(new LedgerTransactionLine
            {
                Id = 3,
                LedgerTransactionId = 1,
                LedgerAccountId = tests.GetTestLedgerAccounts()[2].Id,
                LedgerAccount = tests.GetTestLedgerAccounts()[2],
                Amount = 4000,
                IsDebit = false
            });
            return new LedgerTransaction
            {
                Id = 1,
                Documentation = "Test",
                Description = "Test",
                PostingDate = DateTime.UtcNow,
                LedgerTransactionLines = transactionLines,
                ChartOfAccountsId = 1
            };
        }
    }
}