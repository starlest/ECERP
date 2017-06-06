namespace ECERP.Services.Tests.SupplierProducts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Core.Domain.Products;
    using Core.Domain.Suppliers;
    using Data.Abstract;
    using Moq;
    using Services.Suppliers;
    using Xunit;

    public class SupplierProductServiceTests : ServiceTests
    {
        private readonly ISupplierProductService _supplierProductService;
        private readonly Mock<IRepository> _mockRepo;

        public SupplierProductServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _mockRepo.Setup(x => x.GetOne(It.IsAny<Expression<Func<SupplierProduct, bool>>>()))
                .Returns(this.GetTestSupplierProduct);
            _mockRepo.Setup(x => x.GetById<Supplier>(1)).Returns(this.GetTestSupplier);
            _mockRepo.Setup(x => x.GetById<Product>(1)).Returns(this.GetTestProduct);
            _supplierProductService = new SupplierProductService(_mockRepo.Object);
        }

        public void Can_get_supplierProduct()
        {
            var testSupplierProduct = this.GetTestSupplierProduct();
            var result = _supplierProductService.GetSupplierProduct(1, 1);
            Assert.Equal(testSupplierProduct, result);
        }

        [Fact]
        public void Can_get_supplier_products()
        {
            var testSupplierProducts = this.GetTestSupplierProducts().Select(cs => cs.Product).ToList();
            var results = _supplierProductService.GetSupplierProducts(1);
            CommonHelper.ListsEqual(testSupplierProducts, results);
        }

        // TODO: Need better test case
        [Fact]
        public void Can_get_product_suppliers()
        {
            var testProductSuppliers = this.GetTestSupplierProducts().Select(sp => sp.Supplier).ToList();
            var results = _supplierProductService.GetProductSuppliers(1);
            CommonHelper.ListsEqual(testProductSuppliers, results);
        }

        [Fact]
        public void Can_register()
        {
            Assert.Throws<ArgumentException>(() => _supplierProductService.Register(0, 1));
            Assert.Throws<ArgumentException>(() => _supplierProductService.Register(1, 0));
            _supplierProductService.Register(1, 1);
            _mockRepo.Verify(x => x.Create(It.IsAny<SupplierProduct>()), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void Can_deregister()
        {
            var testSupplierProduct = this.GetTestSupplierProduct();
            _supplierProductService.Deregister(testSupplierProduct.SupplierId, testSupplierProduct.ProductId);
            _mockRepo.Verify(x => x.Delete<SupplierProduct>(testSupplierProduct.Id), Times.Once);
            _mockRepo.Verify(x => x.Save(), Times.Once);
        }
    }
}