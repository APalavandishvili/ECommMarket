using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using ECommMarket.Persistence.Repositories;
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
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();

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
