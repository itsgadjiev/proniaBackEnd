using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using ProniaBackEnd.ViewModels;
using System.Security.Claims;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;

namespace ProniaBackEnd.Controllers
{
    [Route("client")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(AppDbContext appDbContext, UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("auth/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("auth/register")]
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
                Role = Role.RoleEnums.User,
            };

            _appDbContext.Add(user);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        #region Login

        [HttpGet("auth/login")]
        public async Task<IActionResult> Login()
        {
            if (_userService.IsCurrentUserAuthenticated())
            {
                return RedirectToAction("index", "home");
            }


            return View();
        }

        [HttpPost("auth/login")]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _appDbContext.Users.SingleOrDefault(u => u.Email == model.Login);
            if (user is null)
            {
                ModelState.AddModelError("Password", "Data is not valid");
                return View(model);
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                ModelState.AddModelError("Password", "Data is not valid");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
            };

            if (user.Role == Role.RoleEnums.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role.Admin));
            }
            else if (user.Role == Role.RoleEnums.User)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role.User));
            }
            else if (user.Role == Role.RoleEnums.SuperAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role.SuperAdmin));

            }
            else if (user.Role == Role.RoleEnums.Moderator)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role.Moderator));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPricipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPricipal);

            return RedirectToAction("index", "home");
        }

        #endregion

        #region Logout

        [HttpGet("auth/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
