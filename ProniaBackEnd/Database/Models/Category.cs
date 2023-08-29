using Microsoft.Build.Framework;
using ProniaBackEnd.Database.Base;
using ProniaBackEnd.Interfaces;

namespace ProniaBackEnd.Database.Models
{
    public class Category : BaseEntity , IAuditable
    {
        [Required]
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
