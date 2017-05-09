namespace ECERP.Core.Tests.Domain.Cities
{
    using Core.Domain.Cities;
    using Xunit;

    public class CityTests
    {
        [Fact]
        public void Can_create_city()
        {
            var city = new City
            {
                Name = "test"
            };
            Assert.Equal("test", city.Name);
        }
    }
}