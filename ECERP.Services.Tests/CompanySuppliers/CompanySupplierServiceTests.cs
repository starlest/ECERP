namespace ECERP.Services.Tests.CompanySuppliers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Companies;
    using Core.Domain.Suppliers;
    using Data.Abstract;
    using Moq;
    using Services.CompanySuppliers;
    using Xunit;

    public class CompanySupplierServiceTests : ServiceTests
    {
        private readonly ICompanySupplierService _companySupplierService;
        private readonly Mock<IRepository> _mockRepo;

        public CompanySupplierServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.GetOne(It.IsAny<Expression<Func<CompanySupplier, bool>>>()))
                .Returns(this.GetTestCompanySupplier());
            _mockRepo.Setup(x => x.GetById<Company>(1)).Returns(this.GetTestCompany);
            _mockRepo.Setup(x => x.GetById<Supplier>(1)).Returns(this.GetTestSupplier);
            _companySupplierService = new CompanySupplierService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_companySupplier()
        {
            var testCompanySupplier = this.GetTestCompanySupplier();
            var result = _companySupplierService.GetCompanySupplier(1, 1);
            Assert.Equal(testCompanySupplier, result);
        }

        [Fact]
        public void Can_get_company_suppliers()
        {
            var testCompanySuppliers = this.GetTestCompanySuppliers().Select(cs => cs.Supplier).ToList();
            var results = _companySupplierService.GetCompanySuppliers(1);
            CommonHelper.ListsEqual(testCompanySuppliers, results);
        }

        // TODO: Need better test case
        [Fact]
        public void Can_get_supplier_companies()
        {
            var testCompanySuppliers = this.GetTestCompanySuppliers().Select(cs => cs.Company).ToList();
            var results = _companySupplierService.GetSupplierCompanies(1);
            CommonHelper.ListsEqual(testCompanySuppliers, results);
        }

        [Fact]
        public void Can_register()
        {
            Assert.Throws<ArgumentException>(() => _companySupplierService.Register(0, 1));
            Assert.Throws<ArgumentException>(() => _companySupplierService.Register(1, 0));
            _companySupplierService.Register(1, 1);
            _mockRepo.Verify(x => x.Create(It.IsAny<CompanySupplier>()), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void Can_deregister()
        {
            var testCompanySupplier = this.GetTestCompanySupplier();
            _companySupplierService.Deregister(testCompanySupplier.CompanyId, testCompanySupplier.SupplierId);
            _mockRepo.Verify(x => x.Delete<CompanySupplier>(testCompanySupplier.Id), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}