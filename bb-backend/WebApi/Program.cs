using System;
using BLL;
using BLL.AccountService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Middlewares;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog(
        (builderContext, config) =>
        {
            config
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://seq")
                .ReadFrom.Configuration(builderContext.Configuration)
                .WriteTo.Console();
        }
    );


builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddSerilog(dispose: true);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDb(builder.Configuration);

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSpaStaticFiles(opt => opt.RootPath = builder.Environment.WebRootPath);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
        {
            options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/Login");
        }
    );
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(5);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    }
);

var app = builder.Build();

await app.Services.MigrateDb();

if (!builder.Environment.IsProduction())
{
    // TODO: Maybe API sometime
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseCors(
//     builder =>
//     {
//         builder.AllowAnyMethod()
//             .AllowAnyHeader()
//             .SetIsOriginAllowed(_ => true)
//             .AllowCredentials();
//     }
// );

app.UseCors(
    builder => builder
        .WithOrigins(
            "http://localhost",
            "http://localhost:8080",
            "http://localhost:3000",
            "http://birdegop.ru",
            "http://birdegop.ru:8080",
            "https://localhost",
            "https://localhost:8081",
            "https://birdegop.ru",
            "https://birdegop.ru:8081",
            "http://91.190.159.42",
            "https://91.190.159.42"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);

// makes it work behind NGINX
app.UseForwardedHeaders(
    new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    }
);

Console.WriteLine(builder.Environment.ContentRootPath);

app.UseDefaultFiles();
app.UseSpaStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseMiddleware<AuthRequiredMiddleware>();

app.MapControllers();

app.MapFallbackToFile("/index.html");

await app.RunAsync();