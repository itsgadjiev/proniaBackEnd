namespace ProniaBackEnd.Services.abstracts
{
    public interface ICustomEmailService
    {
        public void SendEmail(string recievers, string subject, string messageText) { }
    }
}
