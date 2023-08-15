using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database.Repositories;
using ProniaBackEnd.ViewModels;

namespace ProniaBackEnd.Controllers.client
{
    public class HomeController : Controller
    {
        public SliderRepository _sliderRepository = new SliderRepository();
        public ProductsRepository _productsRepository = new ProductsRepository();
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Products = _productsRepository.GetAll().ToList(),
                Sliders = _sliderRepository.GetAll().OrderBy(x => x.Order).ToList(),
                NewProducts = _productsRepository.GetAll().OrderByDescending(x => x.CreationDate).Take(4).ToList(),
            };

            return View("~/Views/client/home/index.cshtml",homeViewModel);
        }
    }
}