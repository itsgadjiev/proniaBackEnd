using Microsoft.AspNetCore.SignalR;
using ProniaBackEnd.Constants;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Hubs;
using ProniaBackEnd.Services.abstracts;
using ProniaBackEnd.Services.concrets;
using ProniaBackEnd.Templates;
using System.Reflection;

namespace ProniaBackEnd.Services
{
    public class OrderStatusMessageService
    {
        private readonly ICustomEmailService _emailSMTPService;
        private readonly AppDbContext _appDbContext;
        private readonly UserNotificationService _userNotificationService;
        private readonly IHubContext<UserMessageHub> _hubContext;

        public OrderStatusMessageService(ICustomEmailService emailSMTPService, AppDbContext appDbContext, UserNotificationService userNotificationService, IHubContext<UserMessageHub> hubContext)
        {
            _emailSMTPService = emailSMTPService;
            _appDbContext = appDbContext;
            _userNotificationService = userNotificationService;
            _hubContext = hubContext;
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

            try
            {
                string preparedMessage = PrepareMessageForOrder((string)field.GetValue(messageTemplate), order);
                _emailSMTPService.SendEmail(user.Email, "Order Status", preparedMessage);

                var connections = _userNotificationService.GetAllConnectionIds(order.User);
                if (connections.Any())
                {
                    _hubContext
                        .Clients
                        .Clients(connections)
                        .SendAsync("UserOrderStatusNotificationFromAdmin",
                        new
                        {
                            Message = preparedMessage
                        })
                        .Wait();
                }

            }
            catch (Exception e)
            {

                throw new Exception($"This Order status was not found {e.StackTrace}");
            }
        }
    }
}
