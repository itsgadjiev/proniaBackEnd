namespace ProniaBackEnd.Database.Base
{
    public interface IAuditable
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
