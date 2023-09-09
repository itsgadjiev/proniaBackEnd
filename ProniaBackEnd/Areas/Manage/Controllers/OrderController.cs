using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Areas.Manage.ViewModels.orders;
using ProniaBackEnd.Database;
using ProniaBackEnd.Services;
using ProniaBackEnd.ViewModels;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Route("admin/orders")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public OrderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

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
    }
}
