using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using ProniaBackEnd.ViewModels;
using System.Security.Claims;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Controllers
{
    [Route("Client")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AuthController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            if (_appDbContext.Users.Any(u => u.Email == registerViewModel.Email))
            {
                ModelState.AddModelError("Email", "This email already used");
                return View(registerViewModel);
            }

            var user = new User
            {
                Name = registerViewModel.Name,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerViewModel.Password),

            };

            _appDbContext.Add(user);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        #region Login

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (_userService.IsCurrentUserAuthenticated())
            {
                return RedirectToAction("index", "home");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _dbContext.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user is null)
            {
                ModelState.AddModelError("Password", "Email not found");
                return View(model);
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                ModelState.AddModelError("Password", "Password is not valid");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
            };

            if (_userService.DoesUserHaveRole(user, "Super admin"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Super admin"));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPricipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPricipal);

            return RedirectToAction("index", "home");
        }

        #endregion
    }
}
