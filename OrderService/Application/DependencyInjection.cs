using System.Reflection;
using ErrorOr;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Common.Behaviors;
using OrderService.Domain.Agregates;

namespace OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}