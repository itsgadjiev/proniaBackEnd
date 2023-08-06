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
                Products = _productsRepository.GetAll(),
                Sliders = _sliderRepository.GetAll().OrderBy(x=>x.Order).ToList(),
            };

            return View(homeViewModel);
        }

        public IActionResult ProductDetail(int id) 
        {
            Product product = _productsRepository.GetBy(x => x.Id == id);
            if (product is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            return View(product);
        }
    }
}