namespace ProniaBackEnd.Contracts
{
    public class OrderItemStatus
    {
  
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
