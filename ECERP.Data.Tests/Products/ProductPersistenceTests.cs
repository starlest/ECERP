namespace ECERP.Data.Tests.Products
{
    using System.Linq;
    using Xunit;

 
    public class ProductPersistenceTests : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_product()
        {
            var product = this.GetTestProduct();
            var fromDb = SaveAndLoadEntity(this.GetTestProduct());
            Assert.NotNull(fromDb);
            Assert.Equal(product.Id, fromDb.Id);
            Assert.Equal(product.Name, fromDb.Name);
            Assert.Equal(product.PrimaryUnitName, fromDb.PrimaryUnitName);
            Assert.Equal(product.SecondaryUnitName, fromDb.SecondaryUnitName);
            Assert.Equal(product.QuantityPerPrimaryUnit, fromDb.QuantityPerPrimaryUnit);
            Assert.Equal(product.QuantityPerSecondaryUnit, fromDb.QuantityPerSecondaryUnit);
        }
    }
}
