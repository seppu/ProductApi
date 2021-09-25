using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductApi.BusinessLayer.Interfaces;
using ProductApi.BusinessLayer.Services;
using ProductApi.Core.Dto;
using ProductApi.Core.Model;
using ProductApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.UnitTests
{
    [TestClass]
    public class ProductServiceTest
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<ProductService>> _mockLogger;
        public ProductServiceTest()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductService>>();
        }
                
        public IProductService CreateProductService()
        {
            return new ProductService(_mockMapper.Object, _mockLogger.Object, _mockProductRepository.Object);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenInputModelIsNull_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = null;
            await productService.Save(response);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenProductDtoIsNull_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto();
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenProductNameIsNull_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Name = null
            };
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenProductNameIsEmpty_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Name = string.Empty
            };
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenProductNameIsWhiteSpace_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Name = " "
            };
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenDescriptionIsNull_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Name = "Coffee",
                Description = null
            };
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenDescriptionIsEmpty_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Name = "Coffee",
                Description = string.Empty
            };
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenDescriptionIsWhiteSpace_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Name = "Coffee",
                Description = " "
            };
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task SaveProduct_WhenPriceIsNull_ThrowException()
        {
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans",
                Price = null
            };
            await productService.Save(response);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task UpdateProduct_WhenProductDoesntExist_ThrowException()
        {
            _mockMapper.Setup(o => o.Map<ProductResponseDto,Product>(It.IsAny<ProductResponseDto>())).Returns(new Product() {
                Id = Guid.NewGuid(),
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans",
                Price = (decimal)10.99
            });
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Id = Guid.NewGuid(),
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans",
                Price = (decimal?)10.99
            };
            await productService.Save(response);
        }

        [TestMethod]
        public async Task CreateProduct_WhenCorrectDtoProvided_ReturnProductResponseDto()
        {
            _mockMapper.Setup(o => o.Map<ProductResponseDto, Product>(It.IsAny<ProductResponseDto>())).Returns(new Product()
            {
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans",
                Price = (decimal)10.99
            });
            _mockProductRepository.Setup(o => o.Insert(It.IsAny<Product>())).ReturnsAsync(1);
            _mockProductRepository.Setup(o => o.GetProductById(It.IsAny<Guid>())).ReturnsAsync(new Product());
            _mockMapper.Setup(o => o.Map<Product,ProductResponseDto>(It.IsAny<Product>())).Returns(new ProductResponseDto());
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Id = Guid.NewGuid(),
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans",
                Price = (decimal?)10.99
            };
            var ProductResponseDtoResult = await productService.Save(response);
            Assert.IsNotNull(ProductResponseDtoResult);
        }

        [TestMethod]
        public async Task UpdateProduct_WhenProductExist_ReturnProductResponseDto()
        {
            var mockId = Guid.NewGuid();
            _mockMapper.Setup(o => o.Map<ProductResponseDto, Product>(It.IsAny<ProductResponseDto>())).Returns(new Product()
            {
                Id = mockId,
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans - Updated",
                Price = (decimal)10.99
            });
            _mockProductRepository.Setup(o => o.Update(It.IsAny<Product>())).ReturnsAsync(1);
            _mockProductRepository.Setup(o => o.GetProductById(It.IsAny<Guid>())).ReturnsAsync(new Product()
            {
                Id = mockId,
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans - Updated",
                Price = (decimal)10.99
            });
            _mockMapper.Setup(o => o.Map<Product, ProductResponseDto>(It.IsAny<Product>())).Returns(new ProductResponseDto()
            {
                Id = mockId,
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans - Updated",
                Price = (decimal?)10.99
            });
            IProductService productService = CreateProductService();
            ProductResponseDto response = new ProductResponseDto()
            {
                Id = mockId,
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans - Updated",
                Price = (decimal?)10.99
            };
            var productResponseDtoResult = await productService.Save(response);
            Assert.AreEqual(response.Id,productResponseDtoResult.Id);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task GetProduct_WhenIdIsNull_ThrowException()
        {
            IProductService productService = CreateProductService();
            Guid? id = null;
            await productService.GetProduct(id);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public async Task GetProduct_WhenIdIsEmpty_ThrowException()
        {
            IProductService productService = CreateProductService();
            Guid? id = Guid.Empty;
            await productService.GetProduct(id);
        }

        [TestMethod]
        public async Task GetProduct_WhenIdIsCorrect_ReturnProductResponseDto()
        {
            var mockId = Guid.NewGuid();
            _mockProductRepository.Setup(o => o.GetProductById(It.IsAny<Guid>())).ReturnsAsync(new Product()
            {
                Id = mockId,
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans - Updated",
                Price = (decimal)10.99
            });
            _mockMapper.Setup(o => o.Map<Product, ProductResponseDto>(It.IsAny<Product>())).Returns(new ProductResponseDto()
            {
                Id = mockId,
                Name = "Coffee",
                Description = "Honduras - Roasted Arabica Beans - Updated",
                Price = (decimal?)10.99
            });
            IProductService productService = CreateProductService();
            var productResponseDtoResult = await productService.GetProduct(mockId);
            Assert.AreEqual(mockId, productResponseDtoResult.Id);
        }

        [TestMethod]
        public async Task GetAllProduct_ReturnCollectionProductResponseDto()
        {
            ICollection<ProductResponseDto> mockProducts = new List<ProductResponseDto>();
            _mockProductRepository.Setup(o => o.GetAllProducts()).ReturnsAsync(new List<Product>());
            _mockMapper.Setup(o => o.Map<Product, ProductResponseDto>(It.IsAny<Product>())).Returns(new ProductResponseDto());
            IProductService productService = CreateProductService();
            var productResponseDtoResult = await productService.GetAllProducts();
            Assert.AreEqual(mockProducts.GetType(),productResponseDtoResult.GetType());
        }
    }
}
