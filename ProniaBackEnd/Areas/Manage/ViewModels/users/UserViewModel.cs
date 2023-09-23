namespace ProniaBackEnd.Areas.Manage.ViewModels.users
{
    internal class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsOnline { get; set; }
        public string Role { get; set; }
    }
}