namespace ECERP.Data.Tests.Cities
{
    using Xunit;

    public class CityPersistenceTests: PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_city()
        {
            var city = this.GetTestCity();
            var fromDb = SaveAndLoadEntity(this.GetTestCity());
            Assert.NotNull(fromDb);
            Assert.Equal(city.Id, fromDb.Id);
            Assert.Equal(city.Name, fromDb.Name);
        }
    }
}