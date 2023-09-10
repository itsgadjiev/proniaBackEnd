using Microsoft.AspNetCore.Mvc.Rendering;
using ProniaBackEnd.Contracts;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.ViewModels
{
    public class OrderDetailStatusViewModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public List<SelectListItem> OrderItemStatusValues { get; set; }
        public int OrderId { get; set; }
        public int OrderStatusValue { get; set; }
    }
}
