using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database.Repositories;

namespace ProniaBackEnd.Controllers
{
    public class SliderController : Controller
    {
        public SliderRepository _slidederRepository;

        public SliderController()
        {
            _slidederRepository = new SliderRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Slider> sliders = _slidederRepository.GetAll();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string image, string description, string offer, string buttonUrl, byte order, bool offering)
        {
            Slider slider = new Slider(title, description, image, buttonUrl, order);
            
            if (offer != null )
            {
                slider.OfferText = offer.Trim();
                slider.Offering = true;
            }
            
            _slidederRepository.Add(slider);
            return RedirectToAction(nameof(Index));

        }

        


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Slider slider= _slidederRepository.GetBy(x => x.Id == id);
            if (slider is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            _slidederRepository.Delete(slider);
            return RedirectToAction(nameof(Index));
        }
    }
}
