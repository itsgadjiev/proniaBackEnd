using MimeKit;
using ProniaBackEnd.ViewModels.admin.emailMesagges;

namespace ProniaBackEnd.Services.Common
{
    public class RecieversEmailGetter
    {
        public static string[] Handle(string emails)
        {
            List<string> correctEmailsList = new List<string>();
            string[] parts = emails.Split(',');

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Trim().Length == 0)
                {
                    continue;
                }
                else
                {
                    correctEmailsList.Add(parts[i].Trim());
                }
            }

            return correctEmailsList.ToArray();
        }
    }
}
