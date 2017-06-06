namespace ECERP.Data.Tests.Suppliers
{
    using Xunit;

    public class SupplierPersistenceTests : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_supplier()
        {
            var supplier = this.GetTestSupplier();
            var fromDb = SaveAndLoadEntity(this.GetTestSupplier());
            Assert.NotNull(fromDb);
            Assert.Equal(supplier.Id, fromDb.Id);
            Assert.Equal(supplier.Name, fromDb.Name);
            Assert.Equal(supplier.CityId, fromDb.CityId);
            Assert.Equal(supplier.Address, fromDb.Address);
            Assert.Equal(supplier.ContactNumber, fromDb.ContactNumber);
        }

        [Fact]
        public void Can_save_and_load_supplierProduct()
        {
            var supplierProduct = this.GetTestSupplierProduct();
            var fromDb = SaveAndLoadEntity(this.GetTestSupplierProduct());
            Assert.Equal(supplierProduct.Id, fromDb.Id);
            Assert.Equal(supplierProduct.ProductId, fromDb.ProductId);
            Assert.Equal(supplierProduct.SupplierId, fromDb.SupplierId);
        }
    }
}