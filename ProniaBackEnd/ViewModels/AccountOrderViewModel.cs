namespace ProniaBackEnd.ViewModels
{
    public class AccountOrderViewModel
    {
        public int Count { get; set; }
        public double Total { get; set; }
        public string OrderStatus { get; set; }
        public string TracingCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ImageURL { get; set; }
        public int OrderId { get; set; }
    }
}
