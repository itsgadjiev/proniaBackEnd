namespace ProniaBackEnd.Database.Models
{
    public class Role
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Moderator = "Moderator";
        public const string User = "User";

        public enum RoleEnums
        {
            SuperAdmin=0,
            Admin=1,
            Moderator=2,
            User=4
        }
    }
}
