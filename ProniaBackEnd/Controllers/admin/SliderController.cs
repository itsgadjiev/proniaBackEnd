using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Controllers.manage
{
    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public SliderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("~/admin/sliders")]
        public IActionResult Index()
        {
            List<Slider> sliders = _appDbContext.Sliders.ToList();
            return View("~/Views/admin/slider/index.cshtml", sliders);
        }

        [HttpGet("~/admin/sliders/create")]
        public IActionResult Create()
        {
            return View("~/Views/admin/slider/create.cshtml");
        }

        [HttpPost("~/admin/sliders/create")]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/admin/slider/create.cshtml");
            }

            if (slider.OfferText != null)
            {
                slider.Offering = true;
            }

            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet("~/admin/sliders/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(x=>x.Id == id);
            if (slider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _appDbContext.Remove(slider);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("~/admin/sliders/update/{id}")]
        public IActionResult Update(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            return View("~/Views/admin/slider/update.cshtml", slider);

        }
        [HttpPost("~/admin/sliders/update/{id}")]
        public IActionResult Update(Slider slider)
        {
            Slider exSlider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (exSlider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid) { return View("~/Views/admin/slider/update.cshtml", exSlider); }

            exSlider.Title = slider.Title;
            exSlider.Order = slider.Order;
            exSlider.Image = slider.Image;
            exSlider.Description = slider.Description;
            exSlider.ButtonUrl = slider.ButtonUrl;

            slider.Offering = slider.OfferText.Trim().Length == 0 ? true : false;

            _appDbContext.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
    }
}
