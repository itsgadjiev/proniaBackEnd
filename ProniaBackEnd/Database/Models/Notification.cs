using ProniaBackEnd.Database.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProniaBackEnd.Database.Models
{
    public class Notification : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<UserNotification> UserNotifications {  get; set; }
    }
}
