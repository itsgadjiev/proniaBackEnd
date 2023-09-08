using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Services;

namespace ProniaBackEnd.Controllers
{
    [Route("client")]
    public class AccountController : Controller
    {
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {

            return View();
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            return View();
        }

        [HttpGet("Addresses")]
        public IActionResult Addresses()
        {
            return View();
        }

        [HttpGet("AccountDetails")]
        public IActionResult AccountDetails()
        {
            return View();
        }

      
    }
}
