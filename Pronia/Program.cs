using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.mModels;
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
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric= false;
                options.Password.RequireUppercase= true;
                options.Password.RequireLowercase= true;
                options.Password.RequireDigit= true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;
                

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddScoped<LayoutService>();
            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
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