using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.ViewModels;

namespace ProniaBackEnd.Controllers.client
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Products = _appDbContext.Products.ToList(),
                Sliders = _appDbContext.Sliders.OrderBy(x => x.Order).ToList(),
                NewProducts = _appDbContext.Products.OrderByDescending(x => x.CreationDate).Take(4).ToList(),
            };

            return View("~/Views/client/home/index.cshtml", homeViewModel);
        }

        ~HomeController()
        {

        }
    }
}
