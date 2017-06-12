namespace ECERP.Data.Tests.Suppliers
{
    using Xunit;

    public class SupplierSubscriptionPersistenceTests : PersistenceTest
    {
        [Fact]
        public void Can_save_and_load_supplierSubscription()
        {
            var supplierSubscription = this.GetTestSupplierSubscription();
            var fromDb = SaveAndLoadEntity(this.GetTestSupplierSubscription());
            Assert.Equal(supplierSubscription.Id, fromDb.Id);
            Assert.Equal(supplierSubscription.CompanyId, fromDb.CompanyId);
            Assert.Equal(supplierSubscription.SupplierId, fromDb.SupplierId);
            Assert.Equal(supplierSubscription.LedgerAccountId, fromDb.LedgerAccountId);
            Assert.True(supplierSubscription.IsActive);
        }
    }
}