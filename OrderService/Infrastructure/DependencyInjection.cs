using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Messages.Publishers;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddScoped<OrderEventPublisher>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        
        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(configuration["MessageBroker:Host"], host =>
                {
                    host.Username(configuration["MessageBroker:Username"]);
                    host.Password(configuration["MessageBroker:Password"]);
                });
                configurator.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}