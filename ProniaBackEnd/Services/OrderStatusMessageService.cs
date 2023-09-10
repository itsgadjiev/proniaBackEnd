using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services.abstracts;
using ProniaBackEnd.Templates;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata;

namespace ProniaBackEnd.Services
{
    public class OrderStatusMessageService
    {
        private readonly ICustomEmailService _emailSMTPService;
        private readonly AppDbContext _appDbContext;

        public OrderStatusMessageService(ICustomEmailService emailSMTPService,AppDbContext appDbContext)
        {
            _emailSMTPService = emailSMTPService;
            _appDbContext = appDbContext;
        }

        public string PrepareMessageForOrder(string preparedMessage, Order order)
        {
            return preparedMessage
                .Replace(OrderMessageTemplateKeywords.FIRSTNAME, order.User.Name)
                .Replace(OrderMessageTemplateKeywords.LASTNAME, order.User.LastName)
                .Replace(OrderMessageTemplateKeywords.ORDER_NUMBER, order.TracingCode);
        }

        public void SendMessageDueStatusForOrder(Order order)
        {
            MessageTemplate messageTemplate = new MessageTemplate();
            Type type = messageTemplate.GetType();
            FieldInfo field = type.GetField(order.OrderItemStatusValue.ToString().ToUpper() + "_ORDER_" + "AZ")!;
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == order.UserId);
            if (user is null)
            {
                throw new Exception("User doesnt exsists");
            }
            _emailSMTPService.SendEmail(user.Email, "Order Status", PrepareMessageForOrder((string)field.GetValue(messageTemplate), order));
        }
    }
}
