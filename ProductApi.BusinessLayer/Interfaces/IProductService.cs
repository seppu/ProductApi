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
        Task<ProductResponseDto> Save(ProductResponseDto request);

        Task<ProductResponseDto> GetProduct(Guid? id);

        Task<ICollection<ProductResponseDto>> GetAllProducts();
    }
}
