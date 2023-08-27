using MimeKit;

namespace ProniaBackEnd.Services.Common
{
    public class SendMessageToCorrectedEmail
    {
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An error has been occurred while sending email to {correctedEmail[i]}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Inner details {e.InnerException}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }
}
