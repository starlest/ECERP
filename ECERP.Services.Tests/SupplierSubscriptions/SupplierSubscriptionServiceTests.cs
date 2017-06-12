namespace ECERP.Services.Tests.SupplierSubscriptions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Companies;
    using Core.Domain.Suppliers;
    using Data.Abstract;
    using Moq;
    using Services.FinancialAccounting;
    using Services.SupplierSubscriptions;
    using Xunit;

    public class SupplierSubscriptionServiceTests : ServiceTests
    {
        private readonly ISupplierSubscriptionService _supplierSubscriptionService;
        private readonly Mock<IRepository> _mockRepo;

        public SupplierSubscriptionServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.SetupSequence(x => x.GetOne(It.IsAny<Expression<Func<SupplierSubscription, bool>>>()))
                .Returns(this.GetTestSupplierSubscription())
                .Returns(null)
                .Returns(null)
                .Returns(null);
            _mockRepo.Setup(x => x.GetById(1, It.IsAny<Expression<Func<Company, Object>>>())).Returns(this.GetTestCompany);
            _mockRepo.Setup(x => x.GetById<Supplier>(1)).Returns(this.GetTestSupplier);
            var ledgerAccountService = new LedgerAccountService(_mockRepo.Object);
            _supplierSubscriptionService = new SupplierSubscriptionService(_mockRepo.Object, ledgerAccountService);
        }

        [Fact]
        public void Can_get_supplierSubscription()
        {
            var testSupplierSubscription = this.GetTestSupplierSubscription();
            var result = _supplierSubscriptionService.GetSupplierSubscription(1, 1);
            Assert.Equal(testSupplierSubscription, result);
        }

        [Fact]
        public void Can_get_company_suppliers()
        {
            var testCompanySuppliers = this.GetTestSupplierSubscriptions().Select(cs => cs.Supplier).ToList();
            var results = _supplierSubscriptionService.GetCompanySuppliers(1);
            CommonHelper.ListsEqual(testCompanySuppliers, results);
        }

        // TODO: Need better test case
        [Fact]
        public void Can_get_supplierSubscriptions()
        {
            var testSupplierSubscriptions = this.GetTestSupplierSubscriptions().Select(cs => cs.Company).ToList();
            var results = _supplierSubscriptionService.GetSupplierCompanies(1);
            CommonHelper.ListsEqual(testSupplierSubscriptions, results);
        }

        [Fact]
        public void Can_subscribe()
        {
            Assert.Throws<ArgumentException>(() => _supplierSubscriptionService.Subscribe(1, 0));
            Assert.Throws<ArgumentException>(() => _supplierSubscriptionService.Subscribe(0, 1));
            _supplierSubscriptionService.Subscribe(1, 1);
            _mockRepo.Verify(x => x.Create(It.IsAny<SupplierSubscription>()), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void Can_unsubscribe()
        {
            var testSupplierSubscription = this.GetTestSupplierSubscription();
            _supplierSubscriptionService.Unsubscribe(testSupplierSubscription.CompanyId,
                testSupplierSubscription.SupplierId);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}