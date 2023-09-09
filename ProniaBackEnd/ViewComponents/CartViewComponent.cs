using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database;
using ProniaBackEnd.Services;
using ProniaBackEnd.ViewModels;

namespace ProniaBackEnd.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly UserService _userService;
        private readonly AppDbContext _appDbContext;

        public CartViewComponent(UserService userService,AppDbContext appDbContext)
        {
            _userService = userService;
            _appDbContext = appDbContext;
        }

        public IViewComponentResult Invoke()
        {
            if (!_userService.IsCurrentUserAuthenticated())
            {
                return View(new CartViewModel());
            }

            var User = _userService.GetCurrentUser();

            var cartViewModel = new CartViewModel();

            cartViewModel.BasketItems = _appDbContext.BasketItems
                .Where(x => x.Basket.UserId == User.Id && x.IsOrdered == false)
                .Select(x => new CartViewModel.BasketItemViewModel
                {
                    ColorName = x.Color.Name,
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    ProductPrice = x.Product.Price,
                    SizeName = x.Size.Name,
                    ProductQuantity = x.Quantity
                })
                .ToList();

            cartViewModel.Total = _appDbContext.BasketItems.Where(x => x.Basket.UserId == User.Id && x.IsOrdered == false).Sum(bi=>bi.Quantity * bi.Product.Price).Value;


            return View(cartViewModel);
        }

    }
}