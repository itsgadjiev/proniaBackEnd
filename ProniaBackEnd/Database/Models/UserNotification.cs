using MessagePack;
using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class UserNotification : IAuditable
    {
        public User SendingUser { get; set; }
        public int SendingUserId { get; set; }
        public User RecievingUser { get; set; }
        public int RecievingUserId { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
