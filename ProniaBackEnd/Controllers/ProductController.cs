using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database;
using ProniaBackEnd.ViewModels;
using ProniaBackEnd.ViewModels.admin.products;

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

            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel()
            {
                Product = product,
                Sizes = _appDbContext.Size.ToList(),
                Colors = _appDbContext.Color.ToList(),
                Category = _appDbContext.Categories.ToList(),
                ProductCategorys = _appDbContext.ProductCategory.Where(x => x.ProductId == product.Id).ToList(),
                ProductColors = _appDbContext.ProductColor.Where(x => x.ProductId == product.Id).ToList(),
                ProductSizes = _appDbContext.ProductSize.Where(x => x.ProductId == product.Id).ToList(),
            };




            return View(productDetailViewModel);
        }

        [HttpPost]
        public IActionResult AddToBasket(ProductDetailViewModel viewModel, int? id)
        {
            if (id is not null)
            {
                viewModel.Product = new Product();
                viewModel.BasketItem = new BasketItem();
                viewModel.Product.Id = (int)id;
                viewModel.BasketItem.SizeId = _appDbContext.ProductSize.Where(x => x.ProductId == id).Select(x => x.SizeId).FirstOrDefault();
                viewModel.BasketItem.ColorId = _appDbContext.ProductColor.Where(x => x.ProductId == id).Select(x => x.ColorId).FirstOrDefault();

            }
            else
            {
                id = viewModel.Product.Id;
            }

            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == viewModel.Product.Id);
            if (product is null) { return View(NotFoundConstants.NotFoundProniaUrl); }

            BasketItem basketItem = _appDbContext.BasketItems.Where(x => x.ProductId == product.Id && x.SizeId == viewModel.BasketItem.SizeId && x.ColorId == viewModel.BasketItem.ColorId).FirstOrDefault();


            if (basketItem is null)
            {
                basketItem = new BasketItem();
                basketItem.ProductId = product.Id;
                basketItem.ColorId = viewModel.BasketItem.ColorId;
                basketItem.SizeId = viewModel.BasketItem.SizeId;

                if (viewModel.Count is null)
                {
                    basketItem.Quantity = 1;
                }
                else
                {
                    basketItem.Quantity = viewModel.Count;
                }


                _appDbContext.BasketItems.Add(basketItem);

            }
            else
            {
                if (viewModel.Count is null )
                {
                    basketItem.Quantity += 1;
                }
                else
                {
                    basketItem.Quantity += viewModel.Count;
                }
            }

            _appDbContext.SaveChanges();
            return RedirectToAction("Index", "home");
        }



    }
}
