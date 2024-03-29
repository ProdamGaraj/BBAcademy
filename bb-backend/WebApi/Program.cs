﻿using System.Text;
using BLL.AccountService;
using BLL.CartService;
using BLL.CertificateService;
using BLL.ClickService;
using BLL.CourseService;
using BLL.DocumentService;
using BLL.ExamService;
using BLL.Models.Configs;
using BLL.PaymentService;
using BLL.Services;
using BLL.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Configs;
using Newtonsoft.Json;
using Serilog;
using WebApi.Auth;
using WebApi.Middlewares;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));

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

builder.Services.Configure<StaticConfig>(builder.Configuration.GetSection(nameof(StaticConfig)));

Log.Logger.Warning(JsonConvert.SerializeObject(builder.Configuration.GetSection(nameof(PaymentConfig)).Get<PaymentConfig>()));

builder.Services.Configure<PaymentConfig>(builder.Configuration.GetSection(nameof(PaymentConfig)));

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDb(builder.Configuration);

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<ICourseProgressService, CourseProgressService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IClickService, ClickService>();

builder.Services.AddSpaStaticFiles(opt => opt.RootPath = builder.Environment.WebRootPath);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<AuthoriserService>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
            In = ParameterLocation.Header, 
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey 
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            { 
                new OpenApiSecurityScheme 
                { 
                    Reference = new OpenApiReference 
                    { 
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer" 
                    } 
                },
                new string[] { } 
            } 
        });

        options.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Version = "v1",
                Title = "bb API",
                Description = "bb API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            }
        );
    }
);

builder.Services.AddAuthorization();

var app = builder.Build();

await app.Services.MigrateDb();

// if (!builder.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

// makes it work behind NGINX
app.UseForwardedHeaders(
    new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    }
);

app.UseDefaultFiles();
app.UseSpaStaticFiles();

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

app.UseMiddleware<ExceptionCatcherMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("/index.html");

await app.RunAsync();