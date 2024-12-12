using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Profiles;
using EcommMarket.Application.Services;
using ECommMarket.Persistence.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommMarket.Application;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
        return services;
    }
}
