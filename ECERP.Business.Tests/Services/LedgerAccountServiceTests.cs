namespace ECERP.Business.Tests.Services
{
    using System;
    using System.Linq;
    using Models.Entities.FinancialAccounting;
    using Xunit;

    [Collection("Database collection")]
    public class LedgerAccountServiceTests
    {
        private readonly ServiceFixture _fixture;

        #region Constructor
        public LedgerAccountServiceTests(ServiceFixture fixture)
        {
            _fixture = fixture;
        }
        #endregion

        #region Test Cases
        [Fact]
        public void GetAllByCompanyTest()
        {
            var company = _fixture.CompanyService.GetAll().First();
            var ledgerAccounts = _fixture.LedgerAccountService.GetAllByCompany(company.Name);
            // Ensure returned accounts are all of the company's
            var expected = _fixture.ChartOfAccountsService.GetSingleByCompanyName(company.Name).LedgerAccounts.Count;
            Assert.Equal(expected, ledgerAccounts.Count());
        }

        [Fact]
        public void GetLedgerAccountByNameTest()
        {
            var company = _fixture.CompanyService.GetAll().First();
            var cashLedgerAccount = _fixture.LedgerAccountService.GetSingleByName(company.ChartOfAccounts.Id, "Cash");
            Assert.NotNull(cashLedgerAccount);
            var nonExistentAccount = _fixture.LedgerAccountService.GetSingleByName(company.ChartOfAccounts.Id,
                "non-existent");
            Assert.Null(nonExistentAccount);
        }

        [Fact]
        public void GetLedgerAccountPeriodBalance()
        {
            var account = _fixture.LedgerAccountService.GetAll().First();
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var balance = _fixture.LedgerAccountService.GetPeriodBalance(account, year, month - 1);
            Assert.Null(balance);
        }

        [Fact]
        public void GetNewAccountNumberTest()
        {
            var coa = _fixture.ChartOfAccountsService.GetAll().First();
            var newAccountNumber = _fixture.LedgerAccountService.GetNewAccountNumber(coa.Id,
                LedgerAccountGroup.Buildings);
            const int expectedAccountNumber = (int) LedgerAccountGroup.Buildings * 10000 + 1;
            Assert.Equal(expectedAccountNumber, newAccountNumber);
        }

        [Fact]
        public void CreateLedgerAccountTest()
        {
            var coa = _fixture.ChartOfAccountsService.GetAll().First();
            var account_Test = _fixture.LedgerAccountService.GetSingleByName(coa.Id, "test account");
            Assert.Null(account_Test);
            _fixture.LedgerAccountService.Create("test account", "test account", true,
                LedgerAccountType.Liability, LedgerAccountGroup.AccountsPayable, coa.Id, _fixture.Admin.UserName);
            account_Test = _fixture.LedgerAccountService.GetSingleByName(coa.Id, "test account");
            Assert.NotNull(account_Test);
            Assert.Equal(coa.Company.Name, account_Test.ChartOfAccounts.Company.Name);
            Assert.Equal("test account", account_Test.Description);
            Assert.True(account_Test.IsActive);
            Assert.False(account_Test.IsDefault);
            Assert.Equal(LedgerAccountType.Liability, account_Test.Type);
            Assert.Equal(LedgerAccountGroup.AccountsPayable, account_Test.Group);
            Assert.Equal(coa.Id, account_Test.ChartOfAccountsId);
        }
        #endregion
    }
}