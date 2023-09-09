using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                    Count= x.OrderItems.Where(ot => ot.OrderId == x.Id).Count()
                })
                .ToList();


            return View(orders);
        }

        [HttpGet("Addresses")]
        public IActionResult Addresses()
        {
            return View();
        }

        [HttpGet("AccountDetails")]
        public IActionResult AccountDetails()
        {
            return View();
        }

      
    }
}
