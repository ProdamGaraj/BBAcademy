using Backend;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;
using System.Configuration;


CreateWebHostBuilder(args).Build().Run();

IWebHostBuilder CreateWebHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args);

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });

//builder.Services.InitializeRepositories();
//builder.Services.InitializeServices();
var host = WebHost.CreateDefaultBuilder(args);
host.UseSerilog();
//.UseMetrics()
//.UseMetricsWebTracking()
host.ConfigureAppConfiguration(cfg
    => cfg.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false))
                .Build().Run(); ;
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var dc = serviceScope.ServiceProvider.GetRequiredService<BBAcademyDb>();
dc.Database.Migrate();


app.Run();
