using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database;
using ProniaBackEnd.Services;
using ProniaBackEnd.ViewModels;

namespace ProniaBackEnd.Controllers
{
    [Route("client")]
    public class CheckOutController : Controller
    {
        private readonly UserService _userService;
        private readonly AppDbContext _appDbContext;

        public CheckOutController(UserService userService,AppDbContext appDbContext)
        {
            _userService = userService;
            _appDbContext = appDbContext;
        }


        [HttpGet("checkout")]
        public IActionResult Index()
        {
            var user = _userService.GetCurrentUser();
            var cartViewModel = new CartViewModel();

            var basketItems = _appDbContext.BasketItems
                .Where(x => x.Basket.UserId == user.Id)
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

        [HttpPost]
        public IActionResult Order()
        {
            var user = _userService.GetCurrentUser();




            return RedirectToAction("orders", "account");
        }
    }
}
