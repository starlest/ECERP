namespace ECERP.Services.Tests.Companies
{
    using System;
    using System.Linq.Expressions;
    using Core.Domain.Companies;
    using Data.Abstract;
    using ECERP.Services.Companies;
    using Moq;
    using Xunit;

    public class CompanyServiceTests : ServiceTests
    {
        private readonly ICompanyService _companyService;
        private readonly Mock<IRepository> _mockRepo;

        public CompanyServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Company, bool>>>(), null, null, null))
                .Returns(this.GetTestCompanies);
            _mockRepo.Setup(x => x.GetById<Company>(It.IsAny<object>())).Returns(this.GetTestCompany);
            _mockRepo.Setup(x => x.GetOne(It.IsAny<Expression<Func<Company, bool>>>())).Returns(this.GetTestCompany);
            _companyService = new CompanyService(_mockRepo.Object);
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
    }
}