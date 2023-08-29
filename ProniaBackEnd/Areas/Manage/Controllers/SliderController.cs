using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("manage/slider")]
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public SliderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
     
        public IActionResult Index()
        {
            List<Slider> sliders = _appDbContext.Sliders.ToList();
            return View(sliders);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (slider.OfferText != null)
            {
                slider.Offering = true;
            }

            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _appDbContext.Remove(slider);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("update/{id}")]
        public IActionResult Update(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            return View(slider);

        }
        [HttpPost("update/{id}")]
        public IActionResult Update(Slider slider)
        {
            Slider exSlider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (exSlider is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid) { return View( exSlider); }

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
