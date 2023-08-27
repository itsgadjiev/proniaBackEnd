using MimeKit;

namespace ProniaBackEnd.Services.Common
{
    public class RecieversEmailGetter
    {
        public static string[] Handle(string[] emails)
        {
            List<string> correctEmailsList = new List<string>();

            for (int i = 0; i < emails.Length; i++)
            {
                emails[i] = emails[i].Trim();
                correctEmailsList.Add(emails[i]);
            }

            return correctEmailsList.ToArray();
        }
    }
}
