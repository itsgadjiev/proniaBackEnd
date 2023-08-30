using ProniaBackEnd.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace ProniaBackEnd.ViewModels.admin.products
{
    public class ProductAddViewModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
       
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsModified { get; set; }
        public int[] CategoryIds { get; set; }
        public int[] SizeIds { get; set; }
        public int[] ColorIds { get; set; }
        public List<Category> Categories { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Color> Colors { get; set; }
        [Required]
        public IFormFile ImageFormFile{ get; set; }

    }
}
