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
        }
    }
}