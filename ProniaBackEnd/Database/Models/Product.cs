using Microsoft.Build.Framework;
using ProniaBackEnd.Database.Base;
using ProniaBackEnd.Interfaces;

namespace ProniaBackEnd.Database.Models
{
    public class Product : BaseEntity,IAuditable
    {
        public Product()
        {
         
        }

        [Required]
        public string ProductName{ get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsModified { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
