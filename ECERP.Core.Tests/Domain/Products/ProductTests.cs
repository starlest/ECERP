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
            Assert.NotNull(product.ProductSuppliers);
        }

        [Fact]
        public void Can_add_product_supplier()
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
            var productSupplier = new ProductSupplier
            {
                Id = 1,
                ProductId = 1,
                SupplierId = 1,
                PurchasePrice = 1
            };
            product.ProductSuppliers.Add(productSupplier);
            Assert.Equal(1, product.ProductSuppliers.Count);
            Assert.Equal(1, product.ProductSuppliers.First().Id);
        }
    }
}