using System.Net.Http.Headers;
using System.Text;
using AuthorizationService.Dtos;
using AuthorizationService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthorizationService.Controllers;

[ApiController]
public class RegistrationController : Controller
{
    private readonly RegistrationService _registrationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationController"/> class.
    /// </summary>
    /// <param name="registrationService">Service responsible for user registration.</param>
    public RegistrationController(RegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="CreateUserDto">User data transfer object containing registration details.</param>
    /// <returns>An IActionResult indicating success or failure of registration.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto createUserDto)
    {
        var result = await _registrationService.Register(createUserDto);
       
        return result.Success ?  Ok(result) : BadRequest(result.Message);
    }

    
    
}