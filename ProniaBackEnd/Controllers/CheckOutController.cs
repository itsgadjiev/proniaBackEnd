using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEnd.Contracts;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Migrations;
using ProniaBackEnd.Services;
using ProniaBackEnd.ViewModels;

namespace ProniaBackEnd.Controllers
{
    [Route("client")]
    public class CheckOutController : Controller
    {
        private readonly UserService _userService;
        private readonly AppDbContext _appDbContext;
        private readonly OrderCodeGenerator _orderCodeGenerator;
        private readonly OrderStatusMessageService _orderStatusMessageService;

        public CheckOutController(UserService userService, AppDbContext appDbContext, OrderCodeGenerator orderCodeGenerator, OrderStatusMessageService orderStatusMessageService)
        {
            _userService = userService;
            _appDbContext = appDbContext;
            _orderCodeGenerator = orderCodeGenerator;
            _orderStatusMessageService = orderStatusMessageService;
        }


        [HttpGet("checkout")]
        public IActionResult Index()
        {
            var user = _userService.GetCurrentUser();
            var cartViewModel = new CartViewModel();

            var basketItems = _appDbContext.BasketItems
                .Where(x => x.Basket.UserId == user.Id && x.IsOrdered == false)
                .Select(x => new CartViewModel.BasketItemViewModel
                {
                    ColorName = x.Color.Name,
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    ProductPrice = x.Product.Price,
                    SizeName = x.Size.Name,
                    ProductQuantity = x.Quantity,
                    ProductTotal = (x.Quantity * x.Product.Price).Value
                })
                .ToList();

            cartViewModel.BasketItems = basketItems;
            cartViewModel.Total = basketItems.Sum(x => x.ProductPrice * x.ProductQuantity).Value;

            return View(cartViewModel);
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder()
        {
            var user = _userService.GetCurrentUser();

            Order order = new()
            {
                UserId = user.Id,
                OrderItemStatusValue = OrderItemStatus.OrderItemStatusValue.CREATED,
                TracingCode = _orderCodeGenerator.GenarateAndReturnUniqueOrderCode(),
            };

            _appDbContext.Orders.Add(order);

            var orderedBasketItems = _appDbContext.BasketItems
                .Include(x => x.Color)
                .Where(x => x.Basket.UserId == user.Id && x.IsOrdered == false);

            List<OrderItem> orderItems =
               orderedBasketItems
                .Select(x => new OrderItem
                {
                    BasketItem = x,
                    ProductOrderColor = x.Color.Name,
                    ProductOrderDescription = x.Product.Description,
                    ProductOrderName = x.Product.ProductName,
                    ProductOrderPhoto = x.Product.Image,
                    Order = order,
                    ProductOrderPrice = x.Product.Price,
                    ProductOrderQuantity = x.Quantity,
                    ProductOrderSizes = x.Size.Name,

                }).ToList();

            _appDbContext.OrderItems.AddRange(orderItems);

            foreach (var item in orderedBasketItems)
            {
                item.IsOrdered = true;
            }
            


            _appDbContext.SaveChanges();
            _orderStatusMessageService.SendMessageDueStatusForOrder(order);
            return RedirectToAction("orders", "account");
        }
    }
}
