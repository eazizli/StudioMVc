using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudioaMvc.DataContext;
using StudioaMvc.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudioDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 16;
}).AddEntityFrameworkStores<StudioDbContext>().AddDefaultTokenProviders();
var app = builder.Build();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.Run();
