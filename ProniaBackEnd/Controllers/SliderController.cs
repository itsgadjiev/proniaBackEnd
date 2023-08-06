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
            List<Slider> sliders = _slidederRepository.GetAll().ToList();
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
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(buttonUrl) || order == 0)
            {
                return View();
            }

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

        [HttpGet]
        public IActionResult Update(int id)
        {
            Slider slider = _slidederRepository.GetBy(x => x.Id == id);
            if (slider is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            return View(slider);

        }
        [HttpPost]
        public IActionResult Update(int id, string title, string image, string description, string offer, string buttonUrl, byte order, bool offering)
        {
            Slider exSlider = _slidederRepository.GetBy(x => x.Id == id);
            if (exSlider is null) { return View(Constants.Constants.NotFoundApPageUrl); }

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(image) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(buttonUrl) || order == 0) { return View(exSlider); }

            exSlider.Title = title;
            exSlider.Order = order;
            exSlider.Image = image;
            exSlider.Description = description;
            exSlider.ButtonUrl = buttonUrl;

            if (offer != null)
            {
                exSlider.OfferText = offer.Trim();
                exSlider.Offering = true;
            }
            else
            {
                exSlider.Offering = false;
                exSlider.OfferText = String.Empty;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
