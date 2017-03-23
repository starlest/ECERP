namespace ECERP.Data.Tests.Configuration
{
    using Xunit;

    public class CompanySettingPersistenceTest: PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_companySetting()
        {
            var company = this.GetTestCompany();
            var fromDb = SaveAndLoadEntity(this.GetTestCompany());
            Assert.NotNull(fromDb);
            Assert.Equal(company.Id, fromDb.Id);
            Assert.Equal(company.Name, fromDb.Name);
        }
    }
}
