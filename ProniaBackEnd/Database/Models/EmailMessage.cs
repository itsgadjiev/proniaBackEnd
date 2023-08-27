using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class EmailMessage : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Recievers { get; set; }
        public DateTime SendDate { get; set; } 
       
    }
}
