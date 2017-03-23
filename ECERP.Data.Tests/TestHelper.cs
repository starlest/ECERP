namespace ECERP.Data.Tests
{
    using Core.Domain.Companies;
    using Core.Domain.Configuration;

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
    }
}