namespace ECERP.Core.Tests.Domain.Products
{
    using Core.Domain.Products;
    using Xunit;

    public class ProductSubscriptionTests
    {
        [Fact]
        public void Can_create_productSubscription()
        {
            var productSubscription = new ProductSubscription
            {
                Id = 1,
                ProductId = 1,
                SupplierSubscriptionId = 1
            };
            Assert.Equal(1, productSubscription.ProductId);
            Assert.Equal(1, productSubscription.SupplierSubscriptionId);
            Assert.True(productSubscription.IsActive);
        }
    }
}
