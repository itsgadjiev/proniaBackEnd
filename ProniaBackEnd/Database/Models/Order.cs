using ProniaBackEnd.Contracts;
using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class Order : BaseEntity, IAuditable
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string TracingCode { get; set; }
        public OrderItemStatus.OrderItemStatusValue OrderItemStatusValue { get; set; }
    }
}
