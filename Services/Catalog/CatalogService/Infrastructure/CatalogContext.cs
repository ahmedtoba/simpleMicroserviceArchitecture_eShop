using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public DbSet<Product> Products { get; set; }        
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }
    }
}