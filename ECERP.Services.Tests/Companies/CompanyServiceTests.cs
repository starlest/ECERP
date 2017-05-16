namespace ECERP.Services.Tests.Companies
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Companies;
    using Core.Domain.Suppliers;
    using Data.Abstract;
    using Services.Companies;
    using Moq;
    using Services.FinancialAccounting;
    using Xunit;

    public class CompanyServiceTests : ServiceTests
    {
        private readonly ICompanyService _companyService;
        private readonly Mock<IRepository> _mockRepo;

        public CompanyServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.GetAll(null, null, null, It.IsAny<Expression<Func<Company, object>>[]>()))
                .Returns(this.GetTestCompanies);
            _mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), null, null, null))
                .Returns(this.GetTestCompanies);
            _mockRepo.Setup(x => x.GetById<Company>(1)).Returns(this.GetTestCompany);
            _mockRepo.Setup(x => x.GetOne(It.IsAny<Expression<Func<Company, bool>>>())).Returns(this.GetTestCompany);

            var mockLedgerAccountService = new Mock<ILedgerAccountService>();

            _companyService = new CompanyService(_mockRepo.Object, mockLedgerAccountService.Object);
        }

        [Fact]
        public void Can_get_all_companies()
        {
            var testCompanies = this.GetTestCompanies();
            var results = _companyService.GetAllCompanies();
            Assert.True(CommonHelper.ListsEqual(testCompanies, results));
        }

        [Fact]
        public void Can_get_company_by_id()
        {
            var testCompany = this.GetTestCompany();
            var result = _companyService.GetCompanyById(testCompany.Id);
            Assert.Equal(testCompany, result);
        }

        [Fact]
        public void Can_get_company_by_name()
        {
            var testCompany = this.GetTestCompany();
            var result = _companyService.GetCompanyByName(testCompany.Name);
            Assert.Equal(testCompany, result);
        }

        [Fact]
        public void Can_insert_company()
        {
            var testCompany = new Company
            {
                Id = 4,
                Name = "test123"
            };
            _companyService.InsertCompany(testCompany);
            _mockRepo.Verify(x => x.Create(testCompany), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}