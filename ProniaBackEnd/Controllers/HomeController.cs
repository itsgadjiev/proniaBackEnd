using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database.Repositories;
using ProniaBackEnd.ViewModels;
using System.Diagnostics;

namespace ProniaBackEnd.Controllers
{
    public class HomeController : Controller
    {
        public SliderRepository _sliderRepository = new SliderRepository();
        public ProductsRepository _productsRepository = new ProductsRepository();
        public  IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Products = _productsRepository.GetAll().ToList(),
                Sliders = _sliderRepository.GetAll().OrderBy(x => x.Order).ToList(),
                NewProducts = _productsRepository.GetAll().OrderByDescending(x=>x.CreationDate).ToList(),
            };

            return View(homeViewModel);
        }
    }
}