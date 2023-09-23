using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEnd.Areas.Manage.ViewModels.userNotifications;
using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;
using ProniaBackEnd.Services;

namespace ProniaBackEnd.Areas.Manage.Controllers;

[Authorize]
[Route("Manage/Notifications")]
[Area("Manage")]
public class SendNotificationsController : Controller
{
    private readonly AppDbContext _appDbContext;
    private readonly UserService _userService;

    public SendNotificationsController(AppDbContext appDbContext, UserService userService)
    {
        _appDbContext = appDbContext;
        _userService = userService;
    }


    public IActionResult Index()
    {
        var notifications = _appDbContext.UserNotifications.ToList();
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
            UserNotification userNotification = new()
            {
                Notification = notification,
                SendingUserId = _userService.GetCurrentUser().Id,
                RecievingUserId = userId,

            };

            _appDbContext.UserNotifications.Add(userNotification);

        }

        _appDbContext.SaveChanges();


        return RedirectToAction("Index");
    }



}
