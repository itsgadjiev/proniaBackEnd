using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database;

namespace ProniaBackEnd.Controllers.client
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController()
        {
            _appDbContext = new AppDbContext();
        }

        [HttpGet("client/product/detail/{id}")]
        public IActionResult Detail(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundProniaUrl); }

            return View("~/Views/client/products/Detail.cshtml",product);
        }

        ~ProductController()
        {

        }
    }
}
