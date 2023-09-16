using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Database;
using ProniaBackEnd.Services.abstracts;
using ProniaBackEnd.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProniaBackEnd.Controllers
{
    [Authorize]
    [Route("client")]
    public class EmailVerificationController : Controller
    {
        private readonly UserService _userService;
        private readonly AppDbContext _appDbContext;
        private readonly ICustomEmailService _emailService;

        public EmailVerificationController(UserService userService, AppDbContext appDbContext, ICustomEmailService emailService)
        {
            _userService = userService;
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        [HttpPost("EmailVerification/VerifyEmail")]
        public IActionResult SendVerificationLink()
        {
            var user = _userService.GetCurrentUser();
            var token = Guid.NewGuid().ToString();
            var experationDate = DateTime.UtcNow.AddHours(2);

            _appDbContext.UserEmailTokens.Add(new UserEmailToken
            {
                ExpirationDate = experationDate,
                Token = token,
                UserId = user.Id
            });


            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var verificationUrl = $"{baseUrl}/client/EmailVerification/VerifyEmail/{user.Id}/{token}";

            var emailSubject = "Email Verification";
            var emailBody = $"To Verify your Email: {verificationUrl}";


            try
            {
                _emailService.SendEmail(user.Email, emailSubject, emailBody);
                _appDbContext.SaveChanges();
                return RedirectToAction("AccountDetails", "account");
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("EmailVerification/VerifyEmail/{userId}/{token}")]
        public IActionResult VerifyEmail(int userId, string token)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user is null)
                return BadRequest();

            if (user.IsVerifiedEmail)
                return Ok("your email alredy registered");

            var tokenExists = _appDbContext.UserEmailTokens
                .Any(t => t.UserId == userId
                && t.Token == token
                && t.ExpirationDate >= DateTime.UtcNow);

            if (!tokenExists)
                return BadRequest("Something went wrong");


            user.IsVerifiedEmail = true;
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "home");
        }


    }
}
