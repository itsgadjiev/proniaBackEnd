﻿
using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Estensions;
using ProniaBackEnd.Mapper;
using ProniaBackEnd.ViewModels.admin.products;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("manage/products")]
    [Area("Manage")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<ProductListViewModel> productListViewModels = _appDbContext.Products.Select(x => new ProductListViewModel
            {
                Categories = _appDbContext.ProductCategory.Where(pc => pc.ProductId == x.Id).Select(pc => pc.Category).ToList(),
                ProductName = x.ProductName,
                Description = x.Description,
                Id = x.Id,
                Image = x.Image,
                Price = x.Price,
                IsModified = x.IsModified

            }).ToList();
            return View(productListViewModels);
        }

        #region Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            ProductAddViewModel viewModel = new ProductAddViewModel();
            viewModel.Categories = _appDbContext.Categories.ToList();
            return View( viewModel);
        }

        [HttpPost("create")]
        public IActionResult Create(ProductAddViewModel productAddVM)
        {
            productAddVM.Categories = _appDbContext.Categories.ToList();

            if (!ModelState.IsValid)
            {
                return View( productAddVM);
            }

            Product product = new Product()
            {
                ProductName = productAddVM.ProductName,
                Description = productAddVM.Description,
                Color = productAddVM.Color,
                IsModified = productAddVM.IsModified,
                Image = productAddVM.ImageFormFile.SaveFile(_env.WebRootPath, "uploads/images"),
                Price = productAddVM.Price,
                Size = productAddVM.Size,
            };
            _appDbContext.Products.Add(product);


            foreach (var catId in productAddVM.CategoryIds)
            {
                var category = _appDbContext.Categories.FirstOrDefault(x => x.Id == catId);

                if (category is null)
                {
                    ModelState.AddModelError("CategoryIds", "Category not found");
                    return View( productAddVM);
                }

                ProductCategory productCategory = new ProductCategory()
                {
                    CategoryId = catId,
                    Product = product
                };

                _appDbContext.ProductCategory.Add(productCategory);
            }

            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet("update/{id}")]
        public IActionResult Update(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            ProductUpdateViewModel productUpdateViewModel = UpdateMapper<Product, ProductUpdateViewModel>.Handle(product);

            productUpdateViewModel.Categories = _appDbContext.Categories.ToList();
            productUpdateViewModel.CategoryIds = _appDbContext.ProductCategory.Where(x => x.ProductId == product.Id).Select(x => x.CategoryId).ToArray();
            productUpdateViewModel.Image = product.Image;

            return View(productUpdateViewModel);

        }

        [HttpPost("update/{id}")]
        public IActionResult Update(ProductUpdateViewModel productUpdateViewModel)
        {
            Product exProduct = _appDbContext.Products.FirstOrDefault(x => x.Id == productUpdateViewModel.Id);
            if (exProduct is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid)
            {
                productUpdateViewModel.Image = exProduct.Image;
                productUpdateViewModel.Categories = _appDbContext.Categories.ToList();
                productUpdateViewModel.CategoryIds = _appDbContext.ProductCategory.Where(x => x.ProductId == exProduct.Id).Select(x => x.CategoryId).ToArray();
                return View( productUpdateViewModel);
            }

            if (productUpdateViewModel.ImageFile != null)
            {
                productUpdateViewModel.ImageFile.RemoveFile(_env.WebRootPath, "uploads/images", exProduct.Image);
                exProduct.Image = productUpdateViewModel.ImageFile.SaveFile(_env.WebRootPath, "uploads/images");
            }

            if (productUpdateViewModel.CategoryIds != null)
            {
                var removeCats = _appDbContext.ProductCategory.Where(x => x.ProductId == exProduct.Id).ToList();
                _appDbContext.RemoveRange(removeCats);

                var addedCats = productUpdateViewModel.CategoryIds.Select(x => new ProductCategory
                {
                    CategoryId = x,
                    ProductId = exProduct.Id,
                });
                _appDbContext.AddRange(addedCats);
            }

            
            exProduct.Price = productUpdateViewModel.Price;
            exProduct.ProductName = productUpdateViewModel.ProductName;
            exProduct.Description = productUpdateViewModel.Description;
            exProduct.Color = productUpdateViewModel.Color;
            exProduct.Size = productUpdateViewModel.Size;
            exProduct.IsModified = true;

            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _appDbContext.Products.Remove(product);
            FileService.RemoveFile(_env.WebRootPath, "uploads/images", product.Image);

            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
