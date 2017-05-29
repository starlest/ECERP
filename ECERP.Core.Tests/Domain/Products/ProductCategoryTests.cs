namespace ECERP.Core.Tests.Domain.Products
{
    using Core.Domain.Products;
    using Xunit;

    public class ProductCategoryTests
    {
        [Fact]
        public void Can_create_productCategory()
        {
            var productCategory = new ProductCategory
            {
                Name = "test"
            };
            Assert.Equal("test", productCategory.Name);
        }
    }
}
