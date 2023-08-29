using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using System;
using System.Linq;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("Manage/categories")]
    [Area("Manage")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoriesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        public IActionResult Index()
        {
            var categories = _appDbContext.Categories.ToList();
            return View(categories);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/admin/categories/create.cshtml");
        }

        [HttpPost("create")]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/admin/categories/create.cshtml");
            }

            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("update/{id}")]
        public IActionResult Update(int id)
        {
            Category category = _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) { return View(NotFoundConstants.NotFoundApPageUrl); }


            return View("~/Views/admin/categories/update.cshtml", category);
        }

        [HttpPost("update/{id}")]
        public IActionResult Update(Category category)
        {
            Category exCategory = _appDbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (exCategory is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid)
            {
                return View(exCategory);
            }

            exCategory.Name = category.Name;

            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}