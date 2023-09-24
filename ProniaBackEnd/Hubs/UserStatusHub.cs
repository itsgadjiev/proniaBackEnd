using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.concrets;

namespace ProniaBackEnd.Hubs;

[Authorize]
public class UserStatusHub : Hub
{
    private readonly UserOnlineStatusService _userStatusService;
    private readonly UserService _userService;
    private readonly IHubContext<UserStatusHub> _hubContext;
    public UserStatusHub(UserOnlineStatusService alertService, UserService userService, IHubContext<UserStatusHub> hubContext)
    {
        _userStatusService = alertService;
        _userService = userService;
        _hubContext = hubContext;
    }

    public override Task OnConnectedAsync()
    {
        _userStatusService.AddConnectionId(_userService.GetCurrentUser(), Context.ConnectionId);

        var staffUsers = _userService.GetAllStaffMembers();

        foreach (var staffUser in staffUsers)
        {
            var connections = _userStatusService.GetAllConnectionIds(staffUser);

            var userStatusVM = new
            {
                UserId = _userService.GetCurrentUser().Id,
                Status = true
            };

            _hubContext
                .Clients
                .Clients(connections)
                .SendAsync("UserStatus", userStatusVM)
                .Wait();
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var staffUsers = _userService.GetAllStaffMembers();

        foreach (var staffUser in staffUsers)
        {
            var connections = _userStatusService.GetAllConnectionIds(staffUser);

            var userStatusVM = new
            {
                UserId = _userService.GetCurrentUser().Id,
                Status = false
            };

            _hubContext
                .Clients
                .Clients(connections)
                .SendAsync("UserStatus", userStatusVM)
                .Wait();
        }

        _userStatusService.RemoveConnectionId(_userService.GetCurrentUser(),Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}
