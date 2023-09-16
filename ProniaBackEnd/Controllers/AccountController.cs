using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEnd.Database;
using ProniaBackEnd.Services;
using ProniaBackEnd.ViewModels;

namespace ProniaBackEnd.Controllers
{
    [Route("client")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserService _userService;

        public AccountController(AppDbContext appDbContext,UserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {

            return View();
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            var user = _userService.GetCurrentUser();
            var orders = _appDbContext.Orders
                .Where(x => x.UserId == user.Id )
                .Select(x => new AccountOrderViewModel
                {
                    CreatedOn=x.CreatedOn,
                    OrderStatus = x.OrderItemStatusValue.ToString(),
                    TracingCode = x.TracingCode,
                    Total=x.OrderItems.Where(ot=>ot.OrderId==x.Id).Sum(ot=>ot.ProductOrderQuantity * ot.ProductOrderPrice).Value,
                    Count= x.OrderItems.Where(ot => ot.OrderId == x.Id).Count(),
                    OrderId=x.Id
                })
                .ToList();


            return View(orders);
        }

        [HttpGet("order-details/{orderId}")]
        public IActionResult GetOrderDetails(int orderId)
        {
            var order = _appDbContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefault(x => x.Id == orderId && x.UserId == _userService.GetCurrentUser().Id);
            
            if (order == null)
            {
                return NotFound();
            }

            return PartialView("Partials/_OrderDetailsPartial", order);
        }


        [HttpGet("Addresses")]
        public IActionResult Addresses()
        {
            return View();
        }

        [HttpGet("AccountDetails")]
        public IActionResult AccountDetails()
        {
            var user = _userService.GetCurrentUser();
            AccountDetailViewModel accountDetailViewModel = new AccountDetailViewModel
            {
                Email=user.Email,
                Name= user.Name,
                LastName = user.LastName
            };

            return View(accountDetailViewModel);
        }


      
    }
}
