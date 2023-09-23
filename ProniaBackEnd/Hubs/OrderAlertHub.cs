using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.concrets;

namespace ProniaBackEnd.Hubs
{
    [Authorize]
    public class OrderAlertHub : Hub
    {
        private readonly UserOnlineStatusService _userOnlineStatusService;
        private readonly UserService _userService;

        public OrderAlertHub(UserOnlineStatusService userOnlineStatusService,UserService userService)
        {
            _userOnlineStatusService = userOnlineStatusService;
            _userService = userService;
        }
        public override Task OnConnectedAsync()
        {
            _userOnlineStatusService.AddConnectionId(_userService.GetCurrentUser(), Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _userOnlineStatusService.RemoveConnectionId(_userService.GetCurrentUser(), Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
