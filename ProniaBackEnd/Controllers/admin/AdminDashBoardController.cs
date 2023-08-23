using Microsoft.AspNetCore.Mvc;

namespace ProniaBackEnd.Controllers.admin
{
    public class AdminDashBoardController : Controller
    {
        [HttpGet("~/admin")]
        public IActionResult Index()
        {
            return View("~/Views/admin/index.cshtml");
        }
    }
}
