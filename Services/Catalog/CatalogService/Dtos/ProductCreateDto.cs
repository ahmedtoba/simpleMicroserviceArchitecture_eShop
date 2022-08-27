
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}