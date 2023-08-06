using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Product> NewProducts { get; set; }
    }
}
