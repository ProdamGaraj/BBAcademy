using Backend.Services.AccountService.Interfaces;
using Backend.Services.AccountService;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Backend.Services;
using Backend.Services.Interfaces;

namespace Backend
{

    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}