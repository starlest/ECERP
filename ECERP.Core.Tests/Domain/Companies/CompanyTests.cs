namespace ECERP.Core.Tests.Domain.Companies
{
    using System.Linq;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;
    using Xunit;

    public class CompanyTests
    {
        [Fact]
        public void Can_create_company()
        {
            var company = new Company
            {
                Name = "test"
            };
            Assert.Equal("test", company.Name);
            Assert.Equal(1, company.CompanySettings.Count);
            Assert.NotNull(company.ChartOfAccounts);
        }

        [Fact]
        public void Can_add_company_setting()
        {
            var company = new Company
            {
                Name = "test"
            };
            var companySetting = new CompanySetting { Id = 2 };
            company.CompanySettings.Add(companySetting);
            Assert.Equal(2, company.CompanySettings.Count);
            Assert.Equal(2, company.CompanySettings.Last().Id);
        }
    }
}