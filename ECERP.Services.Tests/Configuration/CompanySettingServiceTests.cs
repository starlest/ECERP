namespace ECERP.Services.Tests.Configuration
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Configuration;
    using Data.Abstract;
    using ECERP.Services.Configuration;
    using Moq;
    using Xunit;

    public class CompanySettingServiceTests : ServiceTests
    {
        private readonly ICompanySettingService _companySettingService;
        private readonly Mock<IRepository> _mockRepo;

        public CompanySettingServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<CompanySetting, bool>>>(), null, null, null))
                .Returns(this.GetTestCompanySettings);
            _mockRepo.SetupSequence(x => x.GetOne(It.IsAny<Expression<Func<CompanySetting, bool>>>()))
                .Returns(this.GetTestCompanySetting())
                .Returns(null);
            _companySettingService = new CompanySettingService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_company_settings()
        {
            var testCompanySettings = this.GetTestCompanySettings();
            var results = _companySettingService.GetCompanySettings(1);
            Assert.NotNull(results);
            Assert.Equal(testCompanySettings, results);
            Assert.True(CommonHelper.ListsEqual(testCompanySettings, results));
        }

        [Fact]
        public void Can_get_company_setting_by_key()
        {
            var testCompanySetting = this.GetTestCompanySetting();
            var result = _companySettingService.GetCompanySettingByKey(testCompanySetting.Key,
                testCompanySetting.CompanyId);
            Assert.NotNull(result);
            Assert.Equal(this.GetTestCompanySetting(), result);
        }

        [Fact]
        public void Can_get_company_setting_value_by_key()
        {
            var testCompanySetting = this.GetTestCompanySetting();
            var result = _companySettingService.GetCompanySettingValueByKey<string>(testCompanySetting.Key,
                testCompanySetting.CompanyId);
            Assert.NotNull(result);
            Assert.Equal(testCompanySetting.Value, result);
        }

        [Fact]
        public void Can_set_company_setting()
        {
            var testCompanySetting = this.GetTestCompanySetting();
            _companySettingService.SetCompanySetting(testCompanySetting.Key, "changedValue",
                testCompanySetting.CompanyId);
            _mockRepo.Verify(x => x.Update(It.IsAny<CompanySetting>()), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
            _companySettingService.SetCompanySetting("non-existent", "changedValue", testCompanySetting.CompanyId);
            _mockRepo.Verify(x => x.Create(It.IsAny<CompanySetting>()), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Exactly(2));
        }

        [Fact]
        public void Can_delete_company_setting()
        {
            var testCompanySetting = this.GetTestCompanySetting();
            _companySettingService.DeleteCompanySetting(testCompanySetting);
            _mockRepo.Verify(x => x.Delete(testCompanySetting), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}