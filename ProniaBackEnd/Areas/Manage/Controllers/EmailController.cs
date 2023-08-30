using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.abstracts;
using ProniaBackEnd.Services.Common;
using ProniaBackEnd.Validations;
using ProniaBackEnd.ViewModels.admin.emailMesagges;

namespace ProniaBackEnd.Areas.Manage.Controllers
{
    [Route("manage/email")]
    [Area("Manage")]
    public class EmailController : Controller

    {
        private readonly ICustomEmailService _iEmailService;
        private readonly AppDbContext _appDbContext;
        private readonly EmailMessageValidator _validationRules;

        public EmailController(ICustomEmailService iEmailService, AppDbContext appDbContext, EmailMessageValidator validationRules)
        {
            _iEmailService = iEmailService;
            _appDbContext = appDbContext;
            _validationRules = validationRules;
        }


        public IActionResult Index()
        {
            List<EmailMessage> emailMessages = _appDbContext.EmailMessage.ToList();
            return View(emailMessages);
        }

        [HttpGet("sendEmail")]
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost("sendEmail")]
        public IActionResult SendEmail(EmailMessageAddViewModel emailMessageAddViewModel)
        {
            var validationResult = _validationRules.Validate(emailMessageAddViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(emailMessageAddViewModel);
            }

            EmailMessage emailMessage = new EmailMessage
            {
                Content = emailMessageAddViewModel.Content,
                SendDate = DateTime.UtcNow,
                Title = emailMessageAddViewModel.Title,
                Recievers = RecieversEmailGetter.Handle(emailMessageAddViewModel.Recievers)
            };

            _appDbContext.EmailMessage.Add(emailMessage);
            _appDbContext.SaveChanges();
            _iEmailService.SendEmail(emailMessageAddViewModel.Recievers, emailMessageAddViewModel.Title, emailMessageAddViewModel.Content);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            EmailMessage emailMessage = _appDbContext.EmailMessage.FirstOrDefault(x => x.Id == id);
            if (emailMessage == null) { return View(Constants.NotFoundConstants.NotFoundApPageUrl); }
            return View(emailMessage);
        }
    }
}
