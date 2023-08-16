using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Controllers.manage
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductsController()
        {
            _appDbContext = new AppDbContext();
        }


        [HttpGet("admin/products")]
        public IActionResult Index()
        {
            List<Product> products = _appDbContext.Products.ToList();
            return View("~/Views/admin/products/index.cshtml", products);
        }

        [HttpGet("admin/products/create")]
        public IActionResult Create()
        {
            return View("~/Views/admin/products/create.cshtml");
        }

        [HttpPost("admin/products/create")]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) { return View("~/Views/admin/Products/create.cshtml"); }
         
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("admin/Products/update/{id}")]
        public IActionResult Update(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x=>x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            return View("~/Views/admin/Products/update.cshtml", product);

        }
        [HttpPost("admin/Products/update/{id}")]
        public IActionResult Update(Product product)
        {
            Product exProduct = _appDbContext.Products.FirstOrDefault(x=>x.Id == product.Id);
            if (exProduct is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid)
            {
                return View(exProduct);
            }

            exProduct.Price = product.Price;
            exProduct.Order = product.Order;
            exProduct.ProductName = product.ProductName;
            exProduct.Image = product.Image;
            exProduct.Description = product.Description;
            exProduct.Color = product.Color;
            exProduct.Size = product.Size;
            exProduct.IsModified = true;
            exProduct.LastModifiedDate = DateTime.UtcNow;

            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("admin/Products/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _appDbContext.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
