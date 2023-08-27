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

        public void SendEmail(string toEmail, string subject, string messageText)
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
    }
}
