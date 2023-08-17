using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.ViewModels.admin
{
    public class ProductAddViewModel
    {
        public List<Category> Categories { get; set; }

        public Product Product { get; set; }
    }
}
