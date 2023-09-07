namespace ProniaBackEnd.Database.Models;

public class Basket
{
    public int Id { get; set; }
    public List<BasketItem> BasketItems { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
}
