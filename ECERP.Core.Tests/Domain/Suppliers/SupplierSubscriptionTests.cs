namespace ECERP.Core.Tests.Domain.Suppliers
{
    using Core.Domain.Suppliers;
    using Xunit;

    class SupplierSubscriptionTests
    {
        [Fact]
        public void Can_create_supplierSubscription()
        {
            var supplierSubscription = new SupplierSubscription
            {
                Id = 1,
                CompanyId = 1,
                SupplierId = 1,
                LedgerAccountId = 1
            };
            Assert.Equal(1, supplierSubscription.CompanyId);
            Assert.Equal(1, supplierSubscription.SupplierId);
            Assert.Equal(1, supplierSubscription.LedgerAccountId);
            Assert.True(supplierSubscription.IsActive);
        }
    }
}
