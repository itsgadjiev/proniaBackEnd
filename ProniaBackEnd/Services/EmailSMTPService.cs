using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ProniaBackEnd.Services.abstracts;
using ProniaBackEnd.Services.Common;

namespace ProniaBackEnd.Services
{

    public class EmailSMTPService : IEmailSMTPService
    {
        private readonly IConfiguration _configuration;

        public EmailSMTPService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string[] toEmail, string subject, string messageText)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PRONIA ADMIN", emailSettings["Username"]));

            string[] correctedEmail = RecieversEmailGetter.Handle(toEmail);
            SendMessageToCorrectedEmail.Handle(message, correctedEmail);

            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = messageText };

            using (var client = new SmtpClient())
            {
                client.Connect(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
                client.Authenticate(emailSettings["Username"], emailSettings["Password"]);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        //public void Send(string to, string subject, string html)
        //{

            
        //    var email = new MimeMessage();
        //    email.From.Add(MailboxAddress.Parse("aghabadalov@yandex.com"));

        //    email.To.Add(MailboxAddress.Parse(to));
        //    email.Subject = subject;
        //    email.Body = new TextPart(TextFormat.Html) { Text = html };
           
        //    using var smtp = new SmtpClient();
        //    smtp.Connect("smtp.yandex.ru", 587, SecureSocketOptions.StartTls);
        //    smtp.Authenticate("aghabadalov@yandex.com", "A1g2a3b4");
        //    smtp.Send(email);
        //    smtp.Disconnect(true);

        //}
    }
}
