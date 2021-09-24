using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductApi.BusinessLayer.Interfaces;
using ProductApi.Core.Dto;
using ProductApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.BusinessLayer.Services
{
    public class ProductService: BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IMapper mapper,
          ILogger<ProductService> logger,
          IProductRepository productRepository)
            : base(mapper, logger)
        {
            _productRepository = productRepository;
        }

        public Task<ICollection<ProductResponseDto>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> GetProduct(ProductRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ProductSaveResultDto> Save(ProductRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
