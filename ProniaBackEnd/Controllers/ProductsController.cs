using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database.Repositories;

namespace ProniaBackEnd.Controllers
{
    public class ProductsController : Controller
    {
        public ProductsRepository _productsRepository { get; set; }
        public ProductsController()
        {
            _productsRepository = new ProductsRepository();
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _productsRepository.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string productName, string image, string description, string color, string size, double price, byte order)
        {
            Product product = new Product(productName, image, description, color, size, price, order);
            _productsRepository.Add(product);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            return View(product);

        }
        [HttpPost]
        public IActionResult Update(int id,string productName, string image, string description, string color, string size, double price, byte order)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(Constants.Constants.NotFoundApPageUrl); }
            product.Price = price;
            product.Order = order;
            product.ProductName = productName;
            product.Image = image;
            product.Description = description;
            product.Color = color;
            product.Size = size;
            product.IsModified = true;
            product.LastModifiedDate = DateTime.Now;
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            _productsRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
