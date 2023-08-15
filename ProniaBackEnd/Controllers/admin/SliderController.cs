using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database.Repositories;

namespace ProniaBackEnd.Controllers.manage
{
    public class SliderController : Controller
    {
        public SliderRepository _slidederRepository;

        public SliderController()
        {
            _slidederRepository = new SliderRepository();
        }

        [HttpGet("~/admin/sliders")]
        public IActionResult Index()
        {
            List<Slider> sliders = _slidederRepository.GetAll().ToList();
            return View("~/Views/admin/slider/index.cshtml",sliders);
        }

        [HttpGet("~/admin/sliders/create")]
        public IActionResult Create()
        {
            return View("~/Views/admin/slider/create.cshtml");
        }

        [HttpPost("~/admin/sliders/create")]
        public IActionResult Create(string title, string image, string description, string offer, string buttonUrl, byte order, bool offering)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/admin/slider/create.cshtml");
            }

            Slider slider = new Slider(title, description, image, buttonUrl, order);

            if (offer != null)
            {
                slider.OfferText = offer.Trim();
                slider.Offering = true;
            }

            _slidederRepository.Add(slider);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet("~/admin/sliders/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Slider slider = _slidederRepository.GetBy(x => x.Id == id);
            if (slider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _slidederRepository.Delete(slider);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("~/admin/sliders/update/{id}")]
        public IActionResult Update(int id)
        {
            Slider slider = _slidederRepository.GetBy(x => x.Id == id);
            if (slider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            return View("~/Views/admin/slider/update.cshtml", slider);

        }
        [HttpPost("~/admin/sliders/update/{id}")]
        public IActionResult Update(int id, string title, string image, string description, string offer, string buttonUrl, byte order, bool offering)
        {
            Slider exSlider = _slidederRepository.GetBy(x => x.Id == id);
            if (exSlider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid) { return View("~/Views/admin/slider/update.cshtml", exSlider); }

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
                exSlider.OfferText = string.Empty;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
