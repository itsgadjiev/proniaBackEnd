using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.Common;
using ProniaBackEnd.ViewModels.admin.emailMesagges;

namespace ProniaBackEnd.Controllers.admin
{
    public class EmailController : Controller

    {
        private readonly EmailSMTPService _emailSMTPService;
        private readonly AppDbContext _appDbContext;

        public EmailController(EmailSMTPService emailSMTPService,AppDbContext appDbContext)
        {
            _emailSMTPService = emailSMTPService;
            _appDbContext = appDbContext;
        }

        [HttpGet("~/admin/mesagges")]
        public IActionResult Index()
        {
            List<EmailMessage> emailMessages = _appDbContext.EmailMessage.ToList();
            return View("~/Views/admin/mesagges/index.cshtml", emailMessages);
        }

        [HttpGet("~/admin/sendEmail")]
        public IActionResult SendEmail()
        {

            return View("~/Views/admin/mesagges/SendEmail.cshtml");
        }

        [HttpPost("~/admin/sendEmail")]
        public IActionResult SendEmail(EmailMessageAddViewModel emailMessageAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/admin/mesagges/SendEmail.cshtml", emailMessageAddViewModel);
            }
    
            //string[] recievers = { "gafarov.elvin@gmail.com", " ceyhun592@rambler.ru" };

            EmailMessage emailMessage = new EmailMessage
            {
                Content = emailMessageAddViewModel.Content,
                SendDate = DateTime.UtcNow,
                Title = emailMessageAddViewModel.Title,
                Recievers = RecieversEmailGetter.Handle(emailMessageAddViewModel.Recievers)
            };

            _appDbContext.EmailMessage.Add(emailMessage);
            _appDbContext.SaveChanges();
            _emailSMTPService.SendEmail(emailMessageAddViewModel.Recievers, emailMessageAddViewModel.Title, emailMessageAddViewModel.Content);

            return RedirectToAction(nameof(Index));
        }
    }
}
