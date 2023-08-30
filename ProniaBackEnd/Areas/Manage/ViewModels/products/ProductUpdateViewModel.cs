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
        public double Price { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool IsModified { get; set; }
        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public int[] CategoryIds { get; set; }
        public int[] SizeIds { get; set; }
        public int[] ColorIds { get; set; }

    }
}
