namespace ProniaBackEnd.Areas.Manage.ViewModels.orders
{
    public class OrderListViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public string OrderStatus { get; set; }
        public string TracingCode { get; set; }
        public DateTime CreatedOn { get; set; }
 
    }
}
