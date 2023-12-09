using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProniaBackEnd.Database;
using ProniaBackEnd.Hubs;
using ProniaBackEnd.ViewModels;
using System.Diagnostics;

namespace ProniaBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHubContext<LiveStreamHub> _hubContext;

        public HomeController(AppDbContext appDbContext, IHubContext<LiveStreamHub> hubContext)
        {
            _appDbContext = appDbContext;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Products = _appDbContext.Products.ToList(),
                Sliders = _appDbContext.Sliders.OrderBy(x => x.Order).ToList(),
                NewProducts = _appDbContext.Products.OrderByDescending(x => x.CreatedOn).Take(4).ToList(),
            };

            return View(homeViewModel);
        }



    }
}
