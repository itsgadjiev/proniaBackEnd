using Microsoft.Build.Framework;

namespace ProniaBackEnd.Database.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
