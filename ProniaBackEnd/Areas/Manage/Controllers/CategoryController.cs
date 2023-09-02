using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using System;
using System.Linq;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("Manage/category")]
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        //public async Task<IActionResult> Index()
        //{
        //    var categories = await _appDbContext.Categories.ToListAsync();
        //    return View(categories);
        //}

        public IActionResult Index()
        {
            var categories = _appDbContext.Categories.ToList();
            return View(categories);
        }


        //[HttpGet("getall")]
        //public async Task<IActionResult> GetCategories()
        //{
        //    return View();
        //}


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Category postedCategory)
        {
            //validation

            Category addingCategory = new Category
            {
                Name = postedCategory.Name,
            };


            await _appDbContext.AddAsync(addingCategory);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {

            return Ok();
        }

        //[HttpGet("update/{id}")]
        //public IActionResult Update(int id)
        //{
        //    Category category = _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
        //    if (category is null) { return View(NotFoundConstants.NotFoundApPageUrl); }


        //    return View("~/Views/admin/categories/update.cshtml", category);
        //}

        //[HttpPost("update/{id}")]
        //public IActionResult Update(Category category)
        //{
        //    Category exCategory = _appDbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
        //    if (exCategory is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

        //    if (!ModelState.IsValid)
        //    {
        //        return View(exCategory);
        //    }

        //    exCategory.Name = category.Name;

        //    _appDbContext.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpGet("delete/{id}")]
        //public IActionResult Delete(int id)
        //{
        //    Category category = _appDbContext.Categories.FirstOrDefault(x => x.Id == id);
        //    if (category is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

        //    _appDbContext.Categories.Remove(category);
        //    _appDbContext.SaveChanges();

        //    return RedirectToAction(nameof(Index));
        //}
    }
}