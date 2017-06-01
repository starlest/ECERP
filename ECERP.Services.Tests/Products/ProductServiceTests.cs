namespace ECERP.Services.Tests.Products
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Products;
    using Data.Abstract;
    using Moq;
    using Services.Products;
    using Xunit;

    public class ProductServiceTests : ServiceTests
    {
        private readonly IProductService _productService;
        private readonly Mock<IRepository> _mockRepo;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(
                    x =>
                        x.Get(It.IsAny<Expression<Func<Product, bool>>>(), null, 0, int.MaxValue,
                            It.IsAny<Expression<Func<Product, object>>[]>()))
                .Returns(this.GetTestProducts);
            _mockRepo.Setup(x => x.GetById<Product>(this.GetTestProduct().Id))
                .Returns(this.GetTestProduct);
            _productService = new ProductService(_mockRepo.Object);
        }

        [Fact]
        public void Can_get_products()
        {
            var testProducts = this.GetTestProducts();
            var results = _productService.GetProducts();
            Assert.True(CommonHelper.ListsEqual(testProducts, results));
        }

        [Fact]
        public void Can_get_product_by_id()
        {
            var testProduct = this.GetTestProduct();
            var result = _productService.GetProductById(testProduct.Id);
            Assert.Equal(testProduct, result);
        }

        [Fact]
        public void Can_insert_product()
        {
            var testProduct = this.GetTestProduct();
            _productService.InsertProduct(testProduct);
            _mockRepo.Verify(x => x.Create(testProduct), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);

        }
    }
}