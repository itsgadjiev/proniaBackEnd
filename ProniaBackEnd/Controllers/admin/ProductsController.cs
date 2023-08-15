using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database.Repositories;

namespace ProniaBackEnd.Controllers.manage
{
    public class ProductsController : Controller
    {
        public ProductsRepository _productsRepository { get; set; }
        public ProductsController()
        {
            _productsRepository = new ProductsRepository();
        }


        [HttpGet("admin/products")]
        public IActionResult Index()
        {
            List<Product> products = _productsRepository.GetAll().ToList();
            return View("~/Views/admin/products/index.cshtml", products);
        }

        [HttpGet("admin/products/create")]
        public IActionResult Create()
        {
            return View("~/Views/admin/products/create.cshtml");
        }

        [HttpPost("admin/products/create")]
        public IActionResult Create(string productName, string image, string description, string color, string size, double price, byte order)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/admin/Products/create.cshtml");
            }

            Product product = new Product(productName, image, description, color, size, price, order);
            _productsRepository.Add(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("admin/Products/update/{id}")]
        public IActionResult Update(int id)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            return View("~/Views/admin/Products/update.cshtml", product);

        }
        [HttpPost("admin/Products/update/{id}")]
        public IActionResult Update(int id, string productName, string image, string description, string color, string size, double price, byte order)
        {
            Product exProduct = _productsRepository.GetBy(x => x.Id == id);
            if (exProduct is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid)
            {
                return View(exProduct);
            }

            exProduct.Price = price;
            exProduct.Order = order;
            exProduct.ProductName = productName;
            exProduct.Image = image;
            exProduct.Description = description;
            exProduct.Color = color;
            exProduct.Size = size;
            exProduct.IsModified = true;
            exProduct.LastModifiedDate = DateTime.Now;

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("admin/Products/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _productsRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductDetail(int id)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundProniaUrl); }

            return View(product);
        }
    }
}
