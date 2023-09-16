namespace ProniaBackEnd.Database.Models
{
    public class UserEmailToken
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate{ get; set; }
    }
}
