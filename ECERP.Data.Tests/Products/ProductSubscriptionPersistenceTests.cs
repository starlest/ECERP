namespace ECERP.Data.Tests.Products
{
    using Xunit;

    public class ProductSubscriptionPersistenceTests : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_productSubscription()
        {
            var productSubscription = this.GetTestProductSubscription();
            var fromDb = SaveAndLoadEntity(this.GetTestProductSubscription());
            Assert.Equal(productSubscription.Id, fromDb.Id);
            Assert.Equal(productSubscription.ProductId, fromDb.ProductId);
            Assert.Equal(productSubscription.SupplierSubscriptionId, fromDb.SupplierSubscriptionId);
        }
    }
}