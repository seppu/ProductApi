using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Model;
using ProductApi.Core.Repositories;
using ProductApi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Data.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
