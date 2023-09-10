using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;

namespace ProniaBackEnd.ViewComponents
{
    public class BasketItemCountViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserService _userService;

        public BasketItemCountViewComponent(AppDbContext appDbContext, UserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }
        public IViewComponentResult Invoke()
        {
            int count = 0;
            if (!User.Identity.IsAuthenticated)
            {
                return View(count);
            }
            var userId = _userService.GetCurrentUser().Id;
            count = _appDbContext.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefault(b => b.UserId == userId)
                ?.BasketItems
                .Where(item => item.IsOrdered == false)
                .Count() ?? 0;

            return View(count);
        }
    }
}
