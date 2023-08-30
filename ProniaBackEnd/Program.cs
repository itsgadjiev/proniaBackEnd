using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NETCore.MailKit.Core;
using ProniaBackEnd.Database;
using ProniaBackEnd.Services;
using ProniaBackEnd.Services.abstracts;
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
                .AddTransient<ICustomEmailService, EmailSMTPService>()
                .AddTransient<EmailMessageValidator>();


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}