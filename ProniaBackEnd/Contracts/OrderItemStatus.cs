namespace ProniaBackEnd.Contracts
{
    public class OrderItemStatus
    {
        public const string Created = "Created";
        public const string Confirmed = "Confirmed";
        public const string Rejected = "Rejected";
        public const string Sended = "Sended";
        public const string Completed = "Completed";

        public enum OrderItemStatusValue
        {
            CREATED = 0,
            CONFIRMED = 1,
            REJECTED = 2,
            SENDED = 4,
            COMPLETED = 16
        }

    }
}
