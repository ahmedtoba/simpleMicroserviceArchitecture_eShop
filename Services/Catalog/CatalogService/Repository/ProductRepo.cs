using CatalogService.Infrastructure;
using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly CatalogContext _context;
        public ProductRepo(CatalogContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<int> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return 0;
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
    }
}