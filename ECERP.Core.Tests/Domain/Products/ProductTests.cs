namespace ECERP.Core.Tests.Domain.Products
{
    using System.Linq;
    using Core.Domain.Products;
    using Xunit;

    public class ProductTests
    {
        [Fact]
        public void Can_create_product()
        {
            var product = new Product
            {
                ProductId = "test",
                Name = "test",
                PrimaryUnitName = "test",
                SecondaryUnitName = "test",
                QuantityPerPrimaryUnit = 1,
                QuantityPerSecondaryUnit = 1
            };
            Assert.Equal("test", product.ProductId);
            Assert.Equal("test", product.PrimaryUnitName);
            Assert.Equal("test", product.SecondaryUnitName);
            Assert.Equal(1, product.QuantityPerPrimaryUnit);
            Assert.Equal(1, product.QuantityPerSecondaryUnit);
            Assert.True(product.IsActive);
        }
    }
}