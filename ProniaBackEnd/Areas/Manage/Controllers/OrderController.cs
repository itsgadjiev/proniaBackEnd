using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProniaBackEnd.Areas.Manage.ViewModels.orders;
using ProniaBackEnd.Database;
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

        public OrderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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

            return View(orderDetailStatusVM);
        }

        [HttpPost("Details")]
        public IActionResult UpdateOrderStatus(OrderDetailStatusViewModel OrderDetailStatusViewModel)
        {


            return RedirectToAction(nameof(Index));
        }
    }
}
