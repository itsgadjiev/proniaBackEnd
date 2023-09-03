using MimeKit;

namespace ProniaBackEnd.Services.Common
{

    public class SendMessageToCorrectedEmail
    {
        private readonly ILogger<SendMessageToCorrectedEmail> _logger;

        public SendMessageToCorrectedEmail(ILogger<SendMessageToCorrectedEmail> logger)
        {
            _logger = logger;
        }

        public static void Handle(MimeMessage message, string[] correctedEmail)
        {
            for (int i = 0; i < correctedEmail.Length; i++)
            {
                try
                {
                    message.To.Add(new MailboxAddress("", correctedEmail[i]));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
