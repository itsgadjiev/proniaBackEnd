using ProniaBackEnd.Database.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProniaBackEnd.Database.Models
{
    public class User: BaseEntity , IAuditable
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Role.RoleEnums Role { get; set; }
        public Basket Basket { get; set; }
        public bool IsVerifiedEmail { get; set; }
        public List<UserNotification> UserNotificationsSent { get; set; }
        public List<UserNotification> UserNotificationsReceived { get; set; }


    }
}
