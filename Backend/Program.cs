using Backend;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.EntityFrameworkCore;
using Backend.Services.Repository.Interfaces;
using Backend.Services.Repository;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Logging.ClearProviders();
		builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
		//builder.Services.AddDbContext<BBAcademyDb>();
		builder.Services.AddControllersWithViews();
		builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IAnswerRepository,AnswerRepository> ();
		builder.Services.AddScoped<ICertificateRepository,CertificateRepository> ();
		builder.Services.AddScoped<ICourseRepository,CourseRepository> ();
		builder.Services.AddScoped<IExamRepository,ExamRepository> ();
		builder.Services.AddScoped<ILessonRepository,LessonRepository> ();
		builder.Services.AddScoped<IQuestionRepository,QuestionRepository> ();
        var connection = builder.Configuration.GetConnectionString("DefaultConnection");

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = new PathString("/Account/Login");
				options.AccessDeniedPath = new PathString("/Account/Login");
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
			pattern: "{controller=AccountController}/{action=Index}/{id?}");


		using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
		using var dc = serviceScope.ServiceProvider.GetRequiredService<BBAcademyDb>();



		app.Run();
	}
	public static IWebHost BuildWebHost(string[] args)=>
		WebHost.CreateDefaultBuilder(args)
		.Build();
}