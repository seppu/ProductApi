using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductApi.BusinessLayer.Interfaces;
using ProductApi.Core.Dto;
using ProductApi.Core.Model;
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

        public async Task<ICollection<ProductResponseDto>> GetAllProducts()
        {
            var resultModel = await _productRepository.GetAllProducts();
            return resultModel.Select(re => Mapper.Map<Product, ProductResponseDto>(re)).ToList();
        }

        public async Task<ProductResponseDto> GetProduct(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentNullException("Product Id cannot be null or empty");
            }

            var resultModel = await _productRepository.GetProductById((Guid)id);
            return Mapper.Map<Product, ProductResponseDto>(resultModel);
        }

        public async Task<ProductResponseDto> Save(ProductResponseDto request)
        {
            ProductResponseDto result;
            (var currentModel, var previousModel) = await ValidateRequestAndMapToModel(request);

            var rowsAffected = await UpsertAsync(currentModel, previousModel);
            if (rowsAffected > 0)
            {
                var resultModel = await _productRepository.GetProductById(currentModel.Id);
                result = Mapper.Map<Product, ProductResponseDto>(resultModel);
            }
            else
                result = null;
            return result;
        }

        #region Private
        /// <summary>
        /// Validate the request dto and map to model
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<(Product currentModel, Product previousModel)> ValidateRequestAndMapToModel(ProductResponseDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Product previousModel;
            if (!request.Id.HasValue || request.Id.Value == default)
            {
                previousModel = null;
            }
            else
            {
                previousModel = await _productRepository.GetProductById(request.Id.Value);
            }

            ValidateRequest(request);

            var currentModel = new Product
            {
                Id = request.Id ?? Guid.Empty
            };

            currentModel =  Mapper.Map<ProductResponseDto, Product>(request);

            return (currentModel, previousModel);
        }
        /// <summary>
        /// Validate the request data
        /// </summary>
        /// <param name="request"></param>
        private void ValidateRequest(ProductResponseDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                var message = "Name cannot be null or empty";
                Logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                var message = "Name cannot be null or whitespace";
                Logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            if (string.IsNullOrEmpty(request.Description))
            {
                var message = "Description cannot be null or empty";
                Logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                var message = "Description cannot be null or whitespace";
                Logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            if (!request.Price.HasValue)
            {
                var message = "Price cannot be null";
                Logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            if (request.Price.HasValue)
            {
                if (!decimal.TryParse(request.Price.ToString(),out _))
                {
                    var message = "Price should be of type decimal";
                    Logger.LogError(message);
                    throw new InvalidCastException(message);
                }                    
            }
        }
        /// <summary>
        /// This function calls the repository to insert or update the model
        /// </summary>
        /// <param name="draftModel"></param>
        /// <param name="previousModel"></param>
        /// <returns></returns>
        private async Task<int> UpsertAsync(Product draftModel, Product previousModel)
        {
            int result;
            if (draftModel.HasValidId())
            {
                if (previousModel == null)
                {
                    var message = "Couldnt find a previous model with the proviced ID. Can't update product";
                    Logger.LogError(message);
                    throw new ArgumentNullException(message);
                }
                result = await _productRepository.Update(draftModel);
            }
            else
            {
                result = await _productRepository.Insert(draftModel);
            }
            return result;
        }

        #endregion
    }
}
