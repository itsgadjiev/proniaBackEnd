using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.ViewModels.admin.products;

namespace ProniaBackEnd.Controllers.manage
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductsController()
        {
            _appDbContext = new AppDbContext();
        }


        [HttpGet("admin/products")]
        public IActionResult Index()
        {
            List<Product> products = _appDbContext.Products.ToList();
            return View("~/Views/admin/products/index.cshtml", products);
        }

        #region Crate
        [HttpGet("admin/products/create")]
        public IActionResult Create()
        {
            ProductAddViewModel viewModel = new ProductAddViewModel();
            viewModel.Categories = _appDbContext.Categories.ToList();
            return View("~/Views/admin/products/create.cshtml", viewModel);
        }

        [HttpPost("admin/products/create")]
        public IActionResult Create(ProductAddViewModel productAddVM)
        {
            productAddVM.Categories = _appDbContext.Categories.ToList();

            if (!ModelState.IsValid)
            {
                return View("~/Views/admin/Products/create.cshtml", productAddVM);
            }

            Product product = new Product()
            {
                ProductName = productAddVM.ProductName,
                Description = productAddVM.Description,
                CreationDate = DateTime.UtcNow,
                CategoryId = productAddVM.CategoryId,
                Color = productAddVM.Color,
                IsModified = productAddVM.IsModified,
                Order = productAddVM.Order,
                Image = productAddVM.Image,
                LastModifiedDate = productAddVM.LastModifiedDate,
                Price = productAddVM.Price,
                Size = productAddVM.Size,
            };

            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet("admin/Products/update/{id}")]
        public IActionResult Update(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            ProductUpdateViewModel productUpdateViewModel = new ProductUpdateViewModel
            {
                Categories = _appDbContext.Categories.ToList(),
                ProductName = product.ProductName,
                Price = product.Price,
                Color = product.Color,
                Description = product.Description,
                CreationDate = product.CreationDate,
                Order = product.Order,
                Image = product.Image,
                Size = product.Size,
                CategoryId = product.CategoryId,
                IsModified = product.IsModified,

            };


            return View("~/Views/admin/Products/update.cshtml", productUpdateViewModel);

        }
        [HttpPost("admin/Products/update/{id}")]
        public IActionResult Update(ProductUpdateViewModel productUpdateViewModel)
        {
            Product exProduct = _appDbContext.Products.FirstOrDefault(x => x.Id == productUpdateViewModel.Id);
            if (exProduct is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            if (!ModelState.IsValid)
            {
                return View("~/Views/admin/Products/update.cshtml", productUpdateViewModel);
            }

            exProduct.LastModifiedDate = DateTime.UtcNow;
            exProduct.Price = productUpdateViewModel.Price;
            exProduct.Order = productUpdateViewModel.Order;
            exProduct.ProductName = productUpdateViewModel.ProductName;
            exProduct.Image = productUpdateViewModel.Image;
            exProduct.Description = productUpdateViewModel.Description;
            exProduct.Color = productUpdateViewModel.Color;
            exProduct.Size = productUpdateViewModel.Size;
            exProduct.IsModified = true;

            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete
        [HttpGet("admin/Products/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) { return View(NotFoundConstants.NotFoundApPageUrl); }

            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}
