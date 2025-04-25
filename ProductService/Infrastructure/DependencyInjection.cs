using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}