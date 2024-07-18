using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Product_MVC.Models.Domains
{
    public class ProductDto
    {
        public int Id { get; set; } // Add this line

        [Required, MaxLength(100)]
        public string Name { get; set; } = " ";

        [Required, MaxLength(100)]
        public string Brand { get; set; } = " ";

        [Required, MaxLength(100)]
        public string Category { get; set; } = " ";

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = " ";

        public IFormFile? ImageFile { get; set; }
    }
}
