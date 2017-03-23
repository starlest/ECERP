namespace ECERP.Services.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Domain.Companies;
    using Core.Domain.Configuration;

    public static class TestHelper
    {
        public static List<Company> GetTestCompanies(this ServiceTests tests)
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

        public static List<CompanySetting> GetTestCompanySettings(this ServiceTests tests)
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
    }
}