using Microsoft.AspNetCore.Mvc;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("manage/dashboard")]
    [Area("Manage")]
    public class AdminDashBoardController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
    }
}
