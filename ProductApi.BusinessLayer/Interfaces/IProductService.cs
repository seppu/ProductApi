using ProductApi.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.BusinessLayer.Interfaces
{
    public interface IProductService
    {
        Task<ProductSaveResultDto> Save(ProductRequestDto request);

        Task<ProductResponseDto> GetProduct(ProductRequestDto request);

        Task<ICollection<ProductResponseDto>> GetAllProducts();
    }
}
