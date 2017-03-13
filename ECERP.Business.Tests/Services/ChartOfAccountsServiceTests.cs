namespace ECERP.Business.Tests.Services
{
    using System.Linq;
    using Xunit;

    [Collection("Database collection")]
    public class ChartOfAccountsServiceTests
    {
        #region Private Fields
        private readonly ServiceFixture _fixture;
        #endregion

        #region Constructor
        public ChartOfAccountsServiceTests(ServiceFixture fixture)
        {
            _fixture = fixture;
        }
        #endregion

        #region Test Cases
        [Fact]
        public void GetAllChartsOfAccountsTest()
        {
            var companies = _fixture.CompanyService.GetAll();
            // Should be equal to the number of companies
            Assert.Equal(companies.Count(), _fixture.ChartOfAccountsService.GetAll().Count());
        }

        [Fact]
        public void GetSingleChartsOfAccountsByIdTest()
        {
            var coa = _fixture.ChartOfAccountsService.GetAll().First();
            var returnedCOA = _fixture.ChartOfAccountsService.GetSingleById(-1);
            Assert.Null(returnedCOA);
            returnedCOA = _fixture.ChartOfAccountsService.GetSingleById(coa.Id);
            Assert.NotNull(returnedCOA);
            Assert.Equal(coa, returnedCOA);
        }

        [Fact]
        public void GetSingleChartsOfAccountsByCompanyNameTest()
        {
            var company = _fixture.CompanyService.GetAll().First();
            var returnedCOA = _fixture.ChartOfAccountsService.GetSingleByCompanyName("non-existing company");
            Assert.Null(returnedCOA);
            returnedCOA = _fixture.ChartOfAccountsService.GetSingleByCompanyName(company.Name);
            Assert.NotNull(returnedCOA);
            Assert.Equal(company, returnedCOA.Company);
            var companyCOA = company.ChartOfAccounts;
            Assert.Equal(companyCOA, returnedCOA);
        }
        #endregion
    }
}