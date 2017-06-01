namespace ECERP.Services.Tests.Products
{
    using System;
    using System.Linq;
    using Core;
    using Core.Domain.Products;
    using Data.Abstract;
    using Moq;
    using Services.Products;
    using Xunit;

    public class ProductCategoryServiceTests : ServiceTests
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly Mock<IRepository> _mockRepo;

        public ProductCategoryServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(
                    x => x.GetAll(It.IsAny<Func<IQueryable<ProductCategory>, IOrderedQueryable<ProductCategory>>>(), null, null))
                .Returns(this.GetTestProductCategories);
            _mockRepo.Setup(x => x.GetById<ProductCategory>(1)).Returns(this.GetTestProductCategory);
            _productCategoryService = new ProductCategoryService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_all_productCategories()
        {
            var testProductCategories = this.GetTestProductCategories();
            var results = _productCategoryService.GetAllProductCategories();
            Assert.True(CommonHelper.ListsEqual(testProductCategories, results));
        }

        [Fact]
        public void Can_get_productCategory_by_id()
        {
            var testProductCategory = this.GetTestProductCategory();
            var result = _productCategoryService.GetProductCategoryById(testProductCategory.Id);
            Assert.Equal(testProductCategory, result);
        }

        [Fact]
        public void Can_insert_productCategory()
        {
            var testProductCategory = this.GetTestProductCategory();
            _productCategoryService.InsertProductCategory(testProductCategory);
            _mockRepo.Verify(x => x.Create(testProductCategory), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}
