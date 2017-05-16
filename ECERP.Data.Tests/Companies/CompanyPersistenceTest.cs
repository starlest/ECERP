namespace ECERP.Data.Tests.Companies
{
    using Xunit;

    public class CompanyPersistenceTest : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_company()
        {
            var company = this.GetTestCompany();
            var fromDb = SaveAndLoadEntity(this.GetTestCompany());
            Assert.Equal(company.Id, fromDb.Id);
            Assert.Equal(company.Name, fromDb.Name);
        }

        [Fact]
        public void Can_save_and_load_companySupplier()
        {
            var companySupplier = this.GetTestCompanySupplier();
            var fromDb = SaveAndLoadEntity(this.GetTestCompanySupplier());
            Assert.Equal(companySupplier.Id, fromDb.Id);
            Assert.Equal(companySupplier.CompanyId, fromDb.CompanyId);
            Assert.Equal(companySupplier.SupplierId, fromDb.SupplierId);
        }
    }
}