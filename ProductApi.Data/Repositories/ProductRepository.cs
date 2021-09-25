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
            return await  _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                .ToListAsync();
        }

        public async Task<int> Insert(Product newProduct)
        {
            int rowsAffected;
            using (var transaction = _context.Database.BeginTransaction())
            {
                newProduct.Id = Guid.NewGuid();
                _context.Products.Add(newProduct);
                _context.Entry(newProduct).State = EntityState.Added;
                rowsAffected = await _context.SaveChangesAsync();
                transaction.Commit();
            }
            return rowsAffected;
        }

        public async Task<int> Update(Product updateProduct)
        {
            int rowsAffected;
            using (var transaction = _context.Database.BeginTransaction())
            {
                var product = _context.Products.First(p => p.Id == updateProduct.Id);
                product.Name = updateProduct.Name;
                product.Description = updateProduct.Description;
                product.Price = updateProduct.Price;
                _context.Entry(product).State = EntityState.Modified;

                rowsAffected  = await _context.SaveChangesAsync();
                transaction.Commit();
            }
            return rowsAffected;
        }
    }
}
