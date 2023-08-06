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
            List<Product> products = _productsRepository.GetAll().ToList();
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
            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(size) || price <= 0 || order == 0)
            {
                return View();
            }

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
            Product exProduct = _productsRepository.GetBy(x => x.Id == id);
            if (exProduct is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(size) || price <= 0 || order == 0)
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


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            _productsRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductDetail(int id)
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            return View(product);
        }
    }
}
