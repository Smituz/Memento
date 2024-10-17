using MeriDiaryv2.Data; // Ensure this namespace is correct
using Microsoft.EntityFrameworkCore; // For DbContext
using Microsoft.AspNetCore.Builder; // For IApplicationBuilder
using Microsoft.Extensions.DependencyInjection; // For IServiceCollection

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext with the connection string
builder.Services.AddDbContext<MeriDiaryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Set cookie options as needed
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Use session middleware before authentication and authorization
app.UseSession(); // This line enables session support

app.UseAuthentication(); // Optional: If authentication is added later
app.UseAuthorization();

// Define your route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Landing}/{id?}");

app.Run();
