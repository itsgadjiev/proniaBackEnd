using ProniaBackEnd.Contracts;
using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class OrderItem : BaseEntity, IAuditable
    {
        public BasketItem BasketItem { get; set; }
        public string ProductOrderName { get; set; }
        public double ProductOrderPrice { get; set; }
        public string ProductOrderPhoto { get; set; }
        public double? ProductOrderQuantity { get; set; }
        public string ProductOrderDescription { get; set; }
        public string ProductOrderSizes { get; set; }
        public string ProductOrderColor { get; set; }
        public string ProductOrderCategory { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
