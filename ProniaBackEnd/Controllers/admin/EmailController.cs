using Microsoft.AspNetCore.Mvc;

using ProniaBackEnd.Services;

namespace ProniaBackEnd.Controllers.admin
{
    public class EmailController : Controller
        
    {
        private readonly EmailSMTPService _emailSMTPService;

        public EmailController(EmailSMTPService emailSMTPService)
        {
            _emailSMTPService = emailSMTPService;
        }

        [HttpGet("~/admin/mesagges")]
        public IActionResult Index()
        {
            string[] recievers = { "ceyhun100203@gmail.com"," ceyhun592@rambler.ru" };
            _emailSMTPService.SendEmail(recievers, "40 manatimi ver", "Agilli ol");

            return View("~/Views/admin/mesagges/index.cshtml");
        }
    }
}
