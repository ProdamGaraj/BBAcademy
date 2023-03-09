using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DI
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Bilim");

        services.AddDbContext<BilimContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infrastructure")));
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

        return services;
    }

    public static async Task MigrateDb(this IServiceProvider provider)
    {
        Console.WriteLine("Migrating Db");
        using var serviceScope = provider.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<BilimContext>();
        await context.Database.MigrateAsync();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<BilimContext>>();
        await Seeder.Seed(context, logger);
    }
}