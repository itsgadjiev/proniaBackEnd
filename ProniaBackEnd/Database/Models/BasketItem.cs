using ProniaBackEnd.Database.Base;
using System.Transactions;

namespace ProniaBackEnd.Database.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public double? Quantity { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public int? SizeId { get; set; }
        public Color Color { get; set; }
        public int? ColorId { get; set; }
        public Basket Basket{ get; set; }
        public int BasketId { get; set; }
        public bool IsOrdered { get; set; } = false;
    }
}
