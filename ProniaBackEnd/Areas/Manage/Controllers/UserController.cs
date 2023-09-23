using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Areas.Manage.ViewModels.users;
using ProniaBackEnd.Database;
using ProniaBackEnd.Services.concrets;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("manage/users")]
    [Area("manage")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserOnlineStatusService _userOnlineStatusService;

        public UserController(AppDbContext context, UserOnlineStatusService userOnlineStatusService)
        {
            _context = context;
            _userOnlineStatusService = userOnlineStatusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsConfirmed = u.IsVerifiedEmail,
                    IsOnline = (_userOnlineStatusService.GetAllConnectionIds(u).Count() != 0),
                    Role = u.Role.ToString(),
                })
                .ToList();


            return View(users);
        }
    }

}

