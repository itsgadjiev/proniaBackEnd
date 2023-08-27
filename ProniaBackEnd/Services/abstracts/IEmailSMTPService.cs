namespace ProniaBackEnd.Services.abstracts
{
    public interface IEmailSMTPService
    {
       public void SendEmail(string toEmail, string subject, string messageText, string[] recievers) { }
    }
}
