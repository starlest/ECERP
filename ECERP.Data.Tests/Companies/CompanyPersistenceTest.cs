namespace ECERP.Data.Tests.Companies
{
    using Xunit;

    public class CompanyPersistenceTest : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_companySetting()
        {
            var company = this.GetTestCompanySetting();
            var fromDb = SaveAndLoadEntity(this.GetTestCompanySetting());
            Assert.NotNull(fromDb);
            Assert.Equal(company.Id, fromDb.Id);
            Assert.Equal(company.Key, fromDb.Key);
            Assert.Equal(company.Value, fromDb.Value);
            Assert.Equal(company.CompanyId, fromDb.CompanyId);
        }
    }
}
