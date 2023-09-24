using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProniaBackEnd.Areas.Manage.ViewModels.userNotifications;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Hubs;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.concrets;

namespace ProniaBackEnd.Areas.Manage.Controllers;

[Authorize]
[Route("Manage/Notifications")]
[Area("Manage")]
public class SendNotificationsController : Controller
{
    private readonly AppDbContext _appDbContext;
    private readonly UserService _userService;
    private readonly UserNotificationService _userNotificationService;
    private readonly IHubContext<UserMessageHub> _hubContext;

    public SendNotificationsController(AppDbContext appDbContext, UserService userService, UserNotificationService userNotificationService, IHubContext<UserMessageHub> hubContext)
    {
        _appDbContext = appDbContext;
        _userService = userService;
        _userNotificationService = userNotificationService;
        _hubContext = hubContext;
    }


    public IActionResult Index()
    {
        var notifications = _appDbContext
            .UserNotifications
            .Include(x => x.Notification)
            .Include(x => x.SendingUser)
            .Include(x => x.RecievingUser)
            .ToList();

        return View(notifications);
    }

    [HttpGet("Send")]
    public IActionResult Send()
    {
        UserNotificationViewModel notificationViewModel = new UserNotificationViewModel();
        notificationViewModel.Users = _appDbContext.Users.ToList();
        return View(notificationViewModel);
    }

    [HttpPost("Send")]

    public IActionResult Send(UserNotificationViewModel userNotificationViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userNotificationViewModel);
        }

        Notification notification = new()
        {
            Description = userNotificationViewModel.Desc,
            Title = userNotificationViewModel.Title,
        };

        _appDbContext.Notification.Add(notification);

        foreach (var userId in userNotificationViewModel.SelectedUserIds)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user is null)
            {
                return NotFound();
            }

            UserNotification userNotification = new()
            {
                Notification = notification,
                SendingUserId = _userService.GetCurrentUser().Id,
                RecievingUserId = userId,

            };

            _appDbContext.UserNotifications.Add(userNotification);

            var connections = _userNotificationService.GetAllConnectionIds(user);
            if (connections.Any())
            {
                _hubContext
                    .Clients
                    .Clients(connections)
                    .SendAsync("UserNotificationFromAdmin", 
                    new {
                        Sender=_userService.GetCurrentUser().Name,
                        Reciever=user.Name,
                        Date=DateTime.Now,
                        Title= notification.Title,
                        Desc= notification.Description

                    })
                    .Wait();
            }
        }

        _appDbContext.SaveChanges();




        return RedirectToAction("Index");
    }



}
