namespace ECERP.Core.Tests.Domain.Suppliers
{
    using Core.Domain.Suppliers;
    using Xunit;

    public class SupplierTests
    {
        [Fact]
        public void Can_create_supplier()
        {
            var supplier = new Supplier
            {
                Name = "test",
                Address = "test",
                ContactNumber = "test",
                CityId = 1
            };
            Assert.Equal("test", supplier.Name);
            Assert.Equal("test", supplier.Address);
            Assert.Equal("test", supplier.ContactNumber);
            Assert.Equal(1, supplier.CityId);
            Assert.True(supplier.IsActive);
        }

        [Fact]
        public void Can_add_product_relationship()
        {
            var supplierProduct = new SupplierProduct
            {
                Id = 1,
                ProductId = 1,
                SupplierId = 1
            };
            Assert.Equal(1, supplierProduct.ProductId);
            Assert.Equal(1, supplierProduct.SupplierId);
        }
    }
}