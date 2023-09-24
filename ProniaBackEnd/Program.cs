using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Core;
using ProniaBackEnd.Database;
using ProniaBackEnd.Hubs;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.abstracts;
using ProniaBackEnd.Services.concrets;
using ProniaBackEnd.Validations;


namespace ProniaBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services
                .AddDbContext<AppDbContext>(opt =>
                {
                    string connectionStr = builder.Configuration.GetConnectionString("sql");
                    opt.UseNpgsql(connectionStr);
                })
                .AddScoped<ICustomEmailService, EmailSMTPService>()
                .AddScoped<EmailMessageValidator>()
                .AddScoped<OrderStatusMessageService>()
                .AddScoped<CategoryValidator>()
                .AddScoped<UserService>()
                .AddScoped<OrderCodeGenerator>()
                .AddSingleton<UserOnlineStatusService>()
                .AddSingleton<UserNotificationService>()
                .AddHttpContextAccessor();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = "/client/auth/Login";
             });


            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<UserStatusHub>("/userStatusHub");
            app.MapHub<UserMessageHub>("/userMessageHub");


            app.Run();
        }
    }
}