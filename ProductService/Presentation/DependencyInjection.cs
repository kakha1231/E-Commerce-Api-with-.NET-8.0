using FluentValidation;
using ProductService.Application.Dtos.Request;
using ProductService.Presentation.Validators;

namespace ProductService.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
        
        
        return services;
    }
}