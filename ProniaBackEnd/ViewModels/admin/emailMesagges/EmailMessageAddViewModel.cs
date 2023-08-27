namespace ProniaBackEnd.ViewModels.admin.emailMesagges
{
    public class EmailMessageAddViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Recievers { get; set; }
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
    }
}
