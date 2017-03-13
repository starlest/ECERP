namespace ECERP.Business.Tests.Services
{
    using System.Linq;
    using Xunit;

    [Collection("Database collection")]
    public class CompanyServiceTests
    {
        private readonly ServiceFixture _fixture;

        #region Constructor
        public CompanyServiceTests(ServiceFixture fixture)
        {
            _fixture = fixture;
        }
        #endregion

        #region Test Cases
        [Fact]
        public void GetAllCompaniesTest()
        {
      
            var companies = _fixture.CompanyService.GetAll();
            // Should be equal to the number of Charts of Accounts
            var coas = _fixture.ChartOfAccountsService.GetAll();
            Assert.Equal(companies.Count(), coas.Count());
        }

        [Fact]
        public void GetSingleCompanyByIdTest()
        {
            var company = _fixture.CompanyService.GetAll().First();
            var returnedCompany = _fixture.CompanyService.GetSingleById(-1);
            Assert.Null(returnedCompany);
            returnedCompany = _fixture.CompanyService.GetSingleById(company.Id);
            Assert.NotNull(returnedCompany);
            Assert.Equal(company.Id, returnedCompany.Id);
        }

        [Fact]
        public void GetSingleCompanyByNameTest()
        {
            var company = _fixture.CompanyService.GetAll().First();
            Assert.Null(_fixture.CompanyService.GetSingleByName("non-existing company"));
            Assert.NotNull(_fixture.CompanyService.GetSingleByName(company.Name));
        }

        [Fact]
        public void CreateCompanyTest()
        {
            Assert.Null(_fixture.CompanyService.GetSingleByName("test company"));
            _fixture.CompanyService.CreateCompany("test company");
            var company_Test = _fixture.CompanyService.GetSingleByName("test company");
            Assert.NotNull(company_Test);
            // Check if the default accounts are created
            Assert.Equal(15, company_Test.ChartOfAccounts.LedgerAccounts.Count);
        }
        #endregion
    }
}
