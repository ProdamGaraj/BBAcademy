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
using System.Data.Entity;
using Backend.Services.Interfaces;
using Backend.Services;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.FileProviders;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
var connection=builder.Configuration.GetConnectionString("DefaultConnection");
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BBAcademyDb>(options=>options.UseNpgsql(connection));

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICreationService, CreationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ICertificateTemplateRepository, CertificateTemplateRepository>();


builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
		options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
	});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options=>{
	options.IdleTimeout = TimeSpan.FromMinutes(5);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential= true;
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(builder.Environment.ContentRootPath)
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

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
    await next.Invoke();
});

app.Run();
foreach (var item in app.Urls)
{
    Console.WriteLine(item);
}

//public static IWebHost BuildWebHost(string[] args)=>
//	WebHost.CreateDefaultBuilder(args)
//	.Build();
