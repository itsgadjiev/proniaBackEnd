using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.ViewModels.admin.products
{
    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public bool IsModified { get; set; }
        public int[] CategoryIds { get; set; }
        public List<Category> Categories { get; set; }
    }
}
