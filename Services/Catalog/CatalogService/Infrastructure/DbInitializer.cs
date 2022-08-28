using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CatalogContext>(), isProd);
            }
        }

        public static void SeedData (CatalogContext context, bool isProd)
        {
            if (isProd)
            {
                try 
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Product 1", Price = 1.99M, Image = "1.png", Description = "This is a sample description" },
                    new Product { Name = "Product 2", Price = 2.99M, Image = "2.png", Description = "This is a sample description" },
                    new Product { Name = "Product 3", Price = 3.99M, Image = "3.png", Description = "This is a sample description" },
                    new Product { Name = "Product 4", Price = 4.99M, Image = "4.png", Description = "This is a sample description" },
                    new Product { Name = "Product 5", Price = 5.99M, Image = "5.png", Description = "This is a sample description" }
                );
                context.SaveChanges();
            }
        }
    }
}