using Microsoft.AspNetCore.Mvc.Rendering;
using ProniaBackEnd.Contracts;
using ProniaBackEnd.Database.Models;
using static ProniaBackEnd.Contracts.OrderItemStatus;

namespace ProniaBackEnd.ViewModels
{
    public class OrderDetailStatusViewModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public List<SelectListItem> OrderItemStatusValues { get; set; }
        public int OrderId { get; set; }
        public OrderItemStatusValue OrderStatusValue { get; set; }
    }
}
