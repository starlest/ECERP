namespace ECERP.Core.Tests.Domain.Configuration
{
    using Core.Domain.Configuration;
    using Xunit;

    public class CompanySettingTests
    {
        [Fact]
        public void Can_create_companySetting()
        {
            var companySetting = new CompanySetting
            {
                Key = "Setting1",
                Value = "Value1",
                CompanyId = 1
            };
            Assert.Equal("Setting1", companySetting.Key);
            Assert.Equal("Value1", companySetting.Value);
            Assert.Equal(1, companySetting.CompanyId);
        }
    }
}