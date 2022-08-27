using System.ComponentModel.DataAnnotations;

namespace CatalogService.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}