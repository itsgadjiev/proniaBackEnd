using Microsoft.Build.Framework;
using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class Product : BaseEntity
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
        public byte Order { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsModified { get; set; }

        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }

       
    }
}
