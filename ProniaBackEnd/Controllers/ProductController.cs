using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database;
using ProniaBackEnd.ViewModels;
using ProniaBackEnd.ViewModels.admin.products;
using Microsoft.EntityFrameworkCore;

namespace ProniaBackEnd.Controllers
{
    [Route("client")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("product/detail/{id}")]
        public IActionResult Detail(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundProniaUrl); }

            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel()
            {
                Product = product,
                ProductCategorys = _appDbContext.ProductCategory.Include(x => x.Category).Where(x => x.ProductId == product.Id).ToList(),
                ProductColors = _appDbContext.ProductColor.Include(x => x.Color).Where(x => x.ProductId == product.Id).ToList(),
                ProductSizes = _appDbContext.ProductSize.Include(x => x.Size).Where(x => x.ProductId == product.Id).ToList(),
            };

            ProductDetailBasketViewModel productDetailBasketViewModel = new ProductDetailBasketViewModel();
            productDetailBasketViewModel.ProductDetailViewModel = productDetailViewModel;

            return View(productDetailBasketViewModel);
        }

        [HttpPost("product/addToBasket/{id}")]
        public IActionResult AddToBasket(ProductDetailBasketViewModel productDetailBasketVM,int id)
        {
            var basket = _appDbContext.Baskets.SingleOrDefault();

            if (basket is null)
            {
                basket = new Basket();
                _appDbContext.Baskets.Add(basket);
            }

            var product = _appDbContext.Products.FirstOrDefault(x => x.Id == (productDetailBasketVM != null ? id : productDetailBasketVM.BasketItem.ProductId));
            if (product is null) { return NotFound(); }

            var productSize = _appDbContext.ProductSize
                .FirstOrDefault(ps => ps.ProductId == product.Id
                && (productDetailBasketVM.BasketItem != null ? ps.SizeId == productDetailBasketVM.BasketItem.SizeId : true));

            if (productSize is null) { return NotFound(); }

            var productColor = _appDbContext.ProductColor
               .FirstOrDefault(pc => pc.ProductId == product.Id
               && (productDetailBasketVM.BasketItem != null ? pc.ColorId == productDetailBasketVM.BasketItem.ColorId : true));

            if (productColor is null) { return NotFound(); }


            BasketItem basketItem = _appDbContext.BasketItems
                .FirstOrDefault(x =>
                x.ProductId == product.Id
                && x.ColorId == productColor.ColorId
                && x.SizeId == productSize.SizeId
                && x.Basket == basket);

            if (basketItem is null)
            {
                basketItem = new BasketItem()
                {
                    Basket = basket,
                    ColorId = productColor.ColorId,
                    SizeId = productSize.SizeId,
                    Quantity = (productDetailBasketVM.BasketItem != null ? productDetailBasketVM.BasketItem.Quantity : 1),
                    ProductId = product.Id

                };
                _appDbContext.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.Quantity += (productDetailBasketVM.BasketItem != null ? productDetailBasketVM.BasketItem.Quantity : 1);
            }

            _appDbContext.SaveChanges();
            return RedirectToAction("Index", "home");
        }




    }
}
