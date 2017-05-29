namespace ECERP.Data.Tests.Products
{
    using Xunit;

    public class ProductCategoryPersistenceTests : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_productCategory()
        {
            var productCategory = this.GetTestProductCategory();
            var fromDb = SaveAndLoadEntity(this.GetTestProductCategory());
            Assert.NotNull(fromDb);
            Assert.Equal(productCategory.Id, fromDb.Id);
            Assert.Equal(productCategory.Name, fromDb.Name);
        }
    }
}