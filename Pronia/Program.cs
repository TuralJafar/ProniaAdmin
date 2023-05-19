using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Services;

namespace Pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddScoped<LayoutService>();
            var app = builder.Build();
            app.UseSession();
            app.UseStaticFiles();
            app.MapControllerRoute(
               name: "Areas",
               pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");

            app.MapControllerRoute(
                name: "Default",
                pattern: "{controller=home}/{action=index}/{id?}") ;

            app.Run();
        }
    }
}