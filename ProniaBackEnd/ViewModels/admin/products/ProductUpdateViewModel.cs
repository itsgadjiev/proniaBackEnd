using ProniaBackEnd.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace ProniaBackEnd.ViewModels.admin.products
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public byte Order { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsModified { get; set; }
        public int[] CategoryIds { get; set; }
        public List<Category> Categories { get; set; }

    }
}
