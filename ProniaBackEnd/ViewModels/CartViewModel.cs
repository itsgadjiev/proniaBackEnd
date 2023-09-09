using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.ViewModels
{
    public class CartViewModel
    {
        public List<BasketItemViewModel> BasketItems { get; set; } = new List<BasketItemViewModel>();
        public double Total { get; set; }

        public class BasketItemViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string SizeName { get; set; }
            public string ColorName { get; set; }
            public double ProductPrice { get; set; }
            public double? ProductQuantity { get; set; }
            public string ImageUrl { get; set; }
            public double ProductTotal { get; set; }
        }
    }
}
