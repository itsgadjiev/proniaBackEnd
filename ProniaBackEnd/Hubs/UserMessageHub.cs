using Microsoft.AspNetCore.SignalR;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.concrets;

namespace ProniaBackEnd.Hubs
{
    public class UserMessageHub : Hub
    {
        private readonly UserNotificationService _userNotificationService;
        private readonly UserService _userService;

        public UserMessageHub(UserNotificationService userNotificationService,UserService userService)
        {
            _userNotificationService = userNotificationService;
            _userService = userService;
        }
        public override Task OnConnectedAsync()
        {
            _userNotificationService.AddConnectionId(_userService.GetCurrentUser(), Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _userNotificationService.RemoveConnectionId(_userService.GetCurrentUser(), Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
