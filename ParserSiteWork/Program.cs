using DatabaseWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ParserSiteWork.Models;

namespace ParserSiteWork
{
    public class Program
    {
        public static List<AutorizationModel>? AuthorizedUsers { get; set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AuthorizedUsers = new List<AutorizationModel>();

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<DatabaseContext>(options => {
                options.UseNpgsql(connection);
            });

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
                pattern: "{controller=Authorization}/{action=Login}");

            app.Run();
        }
    }
}
