using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database;

namespace ProniaBackEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("client/product/detail/{id}")]
        public IActionResult Detail(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundProniaUrl); }

            return View(product);
        }

        [HttpPost]
        public IActionResult AddToBasket(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundProniaUrl); }

            BasketItem basketItem = _appDbContext.BasketItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (basketItem is null)
            {
                basketItem = new BasketItem();
                basketItem.ProductId = product.Id;
                basketItem.Quantity = 1;
                basketItem.TotalPrice = product.Price * basketItem.Quantity;

                _appDbContext.BasketItems.Add(basketItem);

            }
            else
            {
                basketItem.Quantity += 1;
                basketItem.TotalPrice = product.Price * basketItem.Quantity;
            }
            _appDbContext.SaveChanges();
            return Ok(basketItem);
        }



    }
}
