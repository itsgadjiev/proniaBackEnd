using ProniaBackEnd.Database.Models;
using System.Diagnostics.CodeAnalysis;

namespace ProniaBackEnd.Areas.Manage.ViewModels.userNotifications
{
    public class UserNotificationViewModel
    {
        public List<User> Users { get; set; }
        [NotNull]
        public string Title { get; set; }
        [NotNull]
        public string Desc { get; set; }
        [NotNull]
        public int[] SelectedUserIds { get; set; }
    }
}
