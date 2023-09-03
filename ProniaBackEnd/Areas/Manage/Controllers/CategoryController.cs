using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEnd.Areas.Manage.ViewModels.categories;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Validations;
using ProniaBackEnd.ViewModels.admin.emailMesagges;
using System;
using System.Linq;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("Manage/category")]
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly CategoryValidator _validationRules;

        public CategoryController(AppDbContext appDbContext, CategoryValidator validationRules)
        {
            _appDbContext = appDbContext;
            _validationRules = validationRules;
        }

        public IActionResult Index()
        {
            var categories = _appDbContext.Categories.ToList();
            return View(categories);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryAddViewModel postedCategoryVM)
        {
            var validationResult = _validationRules.Validate(postedCategoryVM);
            var ListError = new List<ValidationFailure>();
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ListError.Add(error);
                }
                return BadRequest(ListError);
            }

            Category addingCategory = new Category
            {
                Name = postedCategoryVM.Name,
            };


            await _appDbContext.AddAsync(addingCategory);
            await _appDbContext.SaveChangesAsync();

            return Created("postedCategory", addingCategory);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateViewModel categoryVM)
        {
            var validationResult = _validationRules.Validate(categoryVM);
            var ListError = new List<ValidationFailure>();
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ListError.Add(error);
                }
                return BadRequest(ListError);
            }

            Category category = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryVM.Id);

            if (category is null)
            {
                return NotFound();
            }

            category.Name = categoryVM.Name;
            await _appDbContext.SaveChangesAsync();

            return Ok(category);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null) { return NotFound(); }

            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}