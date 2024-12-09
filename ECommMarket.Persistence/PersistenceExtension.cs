using ECommMarket.Persistence.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ECommMarket.Persistence;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MarketDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


        return services;
    }

    public static IApplicationBuilder ConfigurePersistence(this IApplicationBuilder app, MarketDbContext db)
    {
        if (db.Database.IsSqlServer())
        {
            db.Database.EnsureCreatedAsync().Wait();

            db.Seed().Wait();
        }

        return app;
    }
}
