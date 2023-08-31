using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<ProductCategory> ProductCategorys { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Category> Category { get; set; }
        public int? Count { get; set; }
        public BasketItem BasketItem { get; set; }
    }
}
