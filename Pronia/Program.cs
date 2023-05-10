using Microsoft.EntityFrameworkCore;
using Pronia.DAL;

namespace Pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            var app = builder.Build();
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