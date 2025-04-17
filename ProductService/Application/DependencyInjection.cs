using ProductService.Application.Services;

namespace ProductService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductManagementService,ProductManagementService>();
        
        return services;
    }
    
}