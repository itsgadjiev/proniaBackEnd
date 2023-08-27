using MimeKit;

namespace ProniaBackEnd.Services.Common
{
    public class SendMessageToCorrectedEmail
    {
        public static void Handle(MimeMessage message, string[] correctedEmail)
        {
            for (int i = 0; i < correctedEmail.Length; i++)
            {
                message.To.Add(new MailboxAddress("", correctedEmail[i]));
            }
        }
    }
}
