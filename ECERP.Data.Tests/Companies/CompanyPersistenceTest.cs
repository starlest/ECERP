﻿namespace ECERP.Data.Tests.Companies
{
    using Xunit;

    public class CompanyPersistenceTest : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_companySetting()
        {
            var coa = this.GetTestChartOfAccounts();
            var fromDb = SaveAndLoadEntity(this.GetTestChartOfAccounts());
            Assert.NotNull(fromDb);
            Assert.Equal(coa.Id, fromDb.Id);
        }
    }
}
