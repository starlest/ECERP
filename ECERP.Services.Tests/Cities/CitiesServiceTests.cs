namespace ECERP.Services.Tests.Cities
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Cities;
    using Data.Abstract;
    using Moq;
    using Services.Cities;
    using Xunit;

    public class CitiesServiceTests : ServiceTests
    {
        private readonly ICitiesService _citiesService;
        private readonly Mock<IRepository> _mockRepo;

        public CitiesServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(
                    x => x.GetAll<City>(null, null, null))
                .Returns(this.GetTestCities());
            _mockRepo.Setup(x => x.GetById<City>(1)).Returns(this.GetTestCity);
            _mockRepo.Setup(x => x.GetOne(It.IsAny<Expression<Func<City, bool>>>())).Returns(this.GetTestCity);
            _citiesService = new CitiesService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_all_cities()
        {
            var testCities = this.GetTestCities();
            var results = _citiesService.GetAllCities();
            Assert.True(CommonHelper.ListsEqual(testCities, results));
        }

        [Fact]
        public void Can_get_city_by_id()
        {
            var testCity = this.GetTestCity();
            var result = _citiesService.GetCityById(testCity.Id);
            Assert.Equal(testCity, result);
        }

        [Fact]
        public void Can_get_city_by_name()
        {
            var testCity = this.GetTestCity();
            var result = _citiesService.GetCityByName(testCity.Name);
            Assert.Equal(testCity, result);
        }

        [Fact]
        public void Can_insert_city()
        {
            var testCity = this.GetTestCity();
            _citiesService.InsertCity(testCity);
            _mockRepo.Verify(x => x.Create(testCity), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}