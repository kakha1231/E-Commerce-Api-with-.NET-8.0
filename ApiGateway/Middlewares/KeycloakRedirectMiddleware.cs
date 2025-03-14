using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiGateway;

public class KeycloakRedirectMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<KeycloakRedirectMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeycloakRedirectMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware delegate in the pipeline.</param>
    /// <param name="configuration">Configuration settings for Keycloak.</param>
    /// <param name="logger">Logger for logging middleware actions.</param>
    public KeycloakRedirectMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<KeycloakRedirectMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == 401)
        {
            string keycloakLoginPage =
                $"http://localhost:8080/realms/{_configuration["Keycloak:Realm"]}/protocol/openid-connect/auth" +
                "?client_id=my-api-client" +
                "&response_type=code";
            
            _logger.LogInformation("Redirecting unauthorized user to Keycloak login page: {Url}", keycloakLoginPage);
            
            context.Response.Redirect(keycloakLoginPage);
        }
    }
}