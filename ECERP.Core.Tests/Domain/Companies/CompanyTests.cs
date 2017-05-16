namespace ECERP.Core.Tests.Domain.Companies
{
    using System.Linq;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;
    using Core.Domain.Suppliers;
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
            Assert.NotNull(company.ChartOfAccounts);
            Assert.NotNull(company.CompanySettings);
        }

        [Fact]
        public void Can_add_company_setting()
        {
            var company = new Company
            {
                Name = "test"
            };
            var companySetting = new CompanySetting { Id = 1 };
            company.CompanySettings.Add(companySetting);
            Assert.Equal(1, company.CompanySettings.Count);
            Assert.Equal(1, company.CompanySettings.First().Id);
        }

        [Fact]
        public void Can_add_supplier_relationship()
        {
            var companySupplier = new CompanySupplier
            {
                Id = 1,
                CompanyId = 1,
                SupplierId = 1
            };
            Assert.Equal(1, companySupplier.CompanyId);
            Assert.Equal(1, companySupplier.SupplierId);
        }
    }
}