using System.Net.Http.Headers;
using System.Text;
using AuthorizationService.Dtos;
using AuthorizationService.Dtos.Response;
using Newtonsoft.Json;

namespace AuthorizationService.Services;

public class RegistrationService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationService"/> class.
    /// </summary>
    /// <param name="httpClient">HttpClient for sending HTTP requests.</param>
    /// <param name="config">Configuration for Keycloak settings.</param>
    
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public RegistrationService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    /// <summary>
    /// Registers a new user in Keycloak.
    /// </summary>
    /// <param name="CreateUserDto">User data transfer object containing registration details.</param>
    /// <returns>A service response indicating success or failure.</returns>
    public async Task<ServiceResponse<string>> Register(CreateUserDto userDto)
    {
        var adminToken = await GetAdminTokenAsync();
        if (adminToken == null)
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Invalid admin token."
            };
        
        if (await IsEmailExistsAsync(userDto.Email,adminToken))
        {
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "User with the same email already exists.",
            };
        }
        
        var requestData = new
        {
            firstName = userDto.FirstName,
            lastName = userDto.LastName,
            username = $"{userDto.FirstName}_{userDto.LastName}_{Guid.NewGuid().ToString("N")[..6]}", // I don't really need username
            email = userDto.Email,
            enabled = true,
            credentials = new[]
            {
                new { type = "password", value = userDto.Password, temporary = false }
            }
        };
        
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_config["Keycloak:BaseUrl"]}/admin/realms/{_config["Keycloak:Realm"]}/users")
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", adminToken) },
            Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return new ServiceResponse<string>
            {
                Success = true,
                Message = "Registration successful."
            };
        }

        return new ServiceResponse<string>
        {
            Success = false,
            Message = "Registration failed.",
            Data = response.ReasonPhrase
        };
        
    }
    
    /// <summary>
    /// Retrieves an admin token for Keycloak API requests.
    /// </summary>
    /// <returns>The admin token if successful; otherwise, null.</returns>
    private async Task<string?> GetAdminTokenAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_config["Keycloak:BaseUrl"]}/realms/{_config["Keycloak:Realm"]}/protocol/openid-connect/token");
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", "admin-cli" },
            { "grant_type", "password" },
            { "username", _config["Keycloak:AdminUser"] },
            { "password", _config["Keycloak:AdminPassword"] }
        });

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<dynamic>(content)?.access_token;
    }
    
    /// <summary>
    /// Checks if a user with the given email already exists in Keycloak.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <param name="adminToken">The admin token for authentication.</param>
    /// <returns>True if the user exists, otherwise false.</returns>
    private async Task<bool> IsEmailExistsAsync(string email, string adminToken)
    {
        var userCheckRequest = new HttpRequestMessage(HttpMethod.Get, $"{_config["Keycloak:BaseUrl"]}/admin/realms/{_config["Keycloak:Realm"]}/users?email={email}")
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", adminToken) }
        };
        
        var response = await _httpClient.SendAsync(userCheckRequest);
        
        if (response.IsSuccessStatusCode)
        {
            var users = JsonConvert.DeserializeObject<List<dynamic>>(await response.Content.ReadAsStringAsync());
            if (users.Count > 0)
                return true; 
        }
        return false;
    }
}