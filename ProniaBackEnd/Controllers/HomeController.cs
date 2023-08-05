using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProniaBackEnd.Controllers
{
    public class HomeController : Controller
    {
        public  IActionResult Index()
        {
            return View();
        }
    }
}