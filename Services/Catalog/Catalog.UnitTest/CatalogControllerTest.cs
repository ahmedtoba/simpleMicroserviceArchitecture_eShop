using AutoMapper;
using CatalogService.Models;
using CatalogService.Controllers;
using CatalogService.Repository;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Dtos;

namespace Catalog.UnitTest
{
    public class CatalogControllerTest
    {
        private readonly Mock<IProductRepo> _mockProductRepo;

        public CatalogControllerTest()
        {
            _mockProductRepo = new ();
            _mockProductRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Product>());
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProductsCount()
        {

            //automapper configuration
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<ProductReadDto>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(CreateMockProducts());


            // Arrange
            var catalogController = new CatalogController(_mockProductRepo.Object, mockMapper.Object);

            // Act
            var actionResult = await catalogController.GetAllProducts();
            
            // Assert
            Assert.IsType<ActionResult<IEnumerable<ProductReadDto>>>(actionResult);
            var result = actionResult.Result as OkObjectResult;           
            Assert.NotNull(result);
            var actualProducts = Assert.IsAssignableFrom<IEnumerable<ProductReadDto>>(result.Value);
            var expectedProducts = CreateMockProducts();
            Assert.Equal(actualProducts.Count(), expectedProducts.Count());
        }

        [Fact]
        public async Task GetProductByID_ReturnsProduct()
        {
            //Arrange
            var product = new ProductReadDto { Id = 10, Name = "Test Product", Price = 99.00M, Image = "tstProd.png" };
            
            //automapper configuration
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<ProductReadDto>(It.IsAny<Product>()))
                .Returns(product);
            
            _mockProductRepo.Setup(o => o.GetByIdAsync(10)).ReturnsAsync(new Product());
            
            var catalogController = new CatalogController(_mockProductRepo.Object, mockMapper.Object);

            //Act
            var actionResult = await catalogController.GetProductById(10);

            //Assert
            Assert.IsType<ActionResult<ProductReadDto>>(actionResult);
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            var actualProduct = Assert.IsAssignableFrom<ProductReadDto>(result.Value);
            Assert.Equal(product.Id, actualProduct.Id);
        }


        private IEnumerable<ProductReadDto> CreateMockProducts()
        {
            return new List<ProductReadDto>
            {
                new ProductReadDto { Id = 1, Name = "Product 1", Price = 421.00M, Image="prod1.png"},
                new ProductReadDto { Id = 2, Name = "Product 2", Price = 213.00M, Image="prod2.png"},
                new ProductReadDto { Id = 3, Name = "Product 3", Price = 332.00M, Image="prod3.png"},
                new ProductReadDto { Id = 4, Name = "Product 4", Price = 123.00M, Image="prod4.png"}
            };
        }
    }
}