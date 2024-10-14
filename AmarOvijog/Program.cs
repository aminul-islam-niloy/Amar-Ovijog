using AmarOvijog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext and Identity services
builder.Services.AddDbContext<BdGeoServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with ApplicationUser and IdentityRole
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<BdGeoServiceContext>()
        .AddDefaultUI()
    .AddDefaultTokenProviders();


var app = builder.Build();


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area=Admin}/{controller=Divisions}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
