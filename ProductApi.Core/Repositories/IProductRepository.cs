using ProductApi.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(Guid id);
    }
}
