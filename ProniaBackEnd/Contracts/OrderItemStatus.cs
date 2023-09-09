namespace ProniaBackEnd.Contracts
{
    public class OrderItemStatus
    {
        public const string Creted = "Creted";
        public const string Confirmed = "Confirmed";
        public const string Rejected = "Rejected";
        public const string Sended = "Sended";
        public const string Completed = "Completed";

        public enum OrderItemStatusValue
        {

            Creted = 0,
            Confirmed = 1,
            Rejected = 2,
            Sended = 4,
            Completed = 16
        }

    }
}
