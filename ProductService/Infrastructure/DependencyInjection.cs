using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = $"http://{configuration["Keycloak:BaseUrl"]}/realms/{configuration["Keycloak:Realm"]}";
                options.Audience = configuration["Keycloak:Audience"];
                options.RequireHttpsMetadata = false;
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = "role", // <- This tells ASP.NET to look at "role"
                    NameClaimType = "preferred_username"
                };
                options.Events = new JwtBearerEvents   //this code just maps keycloak roles into .net understandable ones
                {
                    OnTokenValidated = context =>
                    {
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        var realmAccess = context.Principal.FindFirst("realm_access");
                        if (realmAccess != null)
                        {
                            var parsed = System.Text.Json.JsonDocument.Parse(realmAccess.Value);
                            if (parsed.RootElement.TryGetProperty("roles", out var roles))
                            {
                                foreach (var role in roles.EnumerateArray())
                                {
                                    identity.AddClaim(new Claim("role", role.GetString()));
                                }
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        return services;
    }
}