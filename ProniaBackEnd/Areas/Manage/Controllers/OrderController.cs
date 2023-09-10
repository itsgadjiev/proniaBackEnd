using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProniaBackEnd.Areas.Manage.ViewModels.orders;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;
using ProniaBackEnd.ViewModels;
using static ProniaBackEnd.Contracts.OrderItemStatus;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Route("manage/orders")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly OrderStatusMessageService _orderStatusMessageService;

        public OrderController(AppDbContext appDbContext, OrderStatusMessageService orderStatusMessageService)
        {
            _appDbContext = appDbContext;
            _orderStatusMessageService = orderStatusMessageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _appDbContext.Orders
                .Select(x => new OrderListViewModel
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    OrderStatus = x.OrderItemStatusValue.ToString(),
                    TracingCode = x.TracingCode,
                    Total = x.OrderItems.Where(ot => ot.OrderId == x.Id).Sum(ot => ot.ProductOrderQuantity * ot.ProductOrderPrice).Value,
                    Count = x.OrderItems.Where(ot => ot.OrderId == x.Id).Count(),

                })
                .ToList();


            return View(orders);

        }

        [HttpGet("Details")]
        public IActionResult OrderDetails(int orderId)
        {
            OrderDetailStatusViewModel orderDetailStatusVM = new OrderDetailStatusViewModel();
            var orderItems = _appDbContext.OrderItems.Where(x => x.OrderId == orderId).ToList();
            var order = _appDbContext.Orders.SingleOrDefault(x => x.Id == orderId);
            orderDetailStatusVM.OrderItems = orderItems;
            orderDetailStatusVM.OrderId = orderId;
            orderDetailStatusVM.OrderItemStatusValues = Enum.GetValues(typeof(OrderItemStatusValue))
            .Cast<OrderItemStatusValue>()
            .Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            })
            .ToList();
            orderDetailStatusVM.OrderStatusValue = order.OrderItemStatusValue;
            return View(orderDetailStatusVM);
        }

        [HttpPost("Details")]
        public IActionResult OrderDetails(OrderDetailStatusViewModel orderDetailStatusViewModel)
        {
            Order order = _appDbContext.Orders.SingleOrDefault(x => x.Id == orderDetailStatusViewModel.OrderId);
            if (order is null) { return BadRequest(); }


            order.OrderItemStatusValue = orderDetailStatusViewModel.OrderStatusValue;
            _orderStatusMessageService.SendMessageDueStatusForOrder(order);
            _appDbContext.SaveChanges();
            
           
            return RedirectToAction(nameof(Index));
        }
    }
}
