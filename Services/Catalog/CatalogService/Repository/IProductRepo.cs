using CatalogService.Models;

namespace CatalogService.Repository
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<int> CreateAsync(Product product);
        Task<int> UpdateAsync(Product product);
        Task<int> DeleteAsync(int id);   
    }
}