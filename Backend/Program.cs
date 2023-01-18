using Backend;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Backend.Services.Repository.Interfaces;
using Backend.Services.Repository;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

builder.Services.AddControllersWithViews();
//var connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<BBAcademyDb>(options=>
//					options.UseSqlServer(connection));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<BBAcademyDb>();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
		options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
	});
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

app.UseCors(builder =>
{
	builder.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(_ => true).AllowCredentials();
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var dc = serviceScope.ServiceProvider.GetRequiredService<BBAcademyDb>();

app.Use(async (context, next) =>
{
	if (!context.User.Identity.IsAuthenticated)
	{
		if (context.Request.Path != "/Account/Register" && context.Request.Path != "/Account/Login" && context.Request.Path != "/")
		{
            context.Response.Redirect("/Account/Login");
        }
	}
    await next.Invoke();
});

app.Run();

//public static IWebHost BuildWebHost(string[] args)=>
//	WebHost.CreateDefaultBuilder(args)
//	.Build();
