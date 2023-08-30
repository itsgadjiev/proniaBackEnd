using System.Transactions;

namespace ProniaBackEnd.Database.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
