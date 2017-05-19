namespace ECERP.Services.Tests.Suppliers
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Companies;
    using Core.Domain.Suppliers;
    using Data.Abstract;
    using Moq;
    using Services.Suppliers;
    using Xunit;

    public class SuppliersServiceTests : ServiceTests
    {
        private readonly ISuppliersService _suppliersService;
        private readonly Mock<IRepository> _mockRepo;

        public SuppliersServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(
                    x =>
                        x.Get(It.IsAny<Expression<Func<Supplier, bool>>>(), null, 0, int.MaxValue,
                            It.IsAny<Expression<Func<Supplier, object>>[]>()))
                .Returns(this.GetTestSuppliers);
            _mockRepo.Setup(x => x.GetById<Company>(1)).Returns(this.GetTestCompany);
            _mockRepo.Setup(x => x.GetById<Supplier>(1, s => s.City)).Returns(this.GetTestSupplier);
            _suppliersService = new SuppliersService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_suppliers()
        {
            var testSuppliers = this.GetTestSuppliers();
            var results = _suppliersService.GetSuppliers();
            Assert.True(CommonHelper.ListsEqual(testSuppliers, results));
        }

        [Fact]
        public void Can_insert_supplier()
        {
            var testSupplier = this.GetTestSupplier();
            _suppliersService.InsertSupplier(testSupplier);
            _mockRepo.Verify(x => x.Create(testSupplier), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }


        [Fact]
        public void Can_register_supplier_to_company()
        {
            Assert.Throws<ArgumentException>(() => _suppliersService.RegisterSupplierToCompany(0, 1));
            Assert.Throws<ArgumentException>(() => _suppliersService.RegisterSupplierToCompany(1, 0));
            _suppliersService.RegisterSupplierToCompany(1, 1);
            _mockRepo.Verify(x => x.Create(It.IsAny<CompanySupplier>()), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}