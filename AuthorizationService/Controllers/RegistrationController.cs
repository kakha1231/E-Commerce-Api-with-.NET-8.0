using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using AuthorizationService.Dtos;
using AuthorizationService.Dtos.Request;
using AuthorizationService.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthorizationService.Controllers;

[ApiController]
public class RegistrationController : Controller
{
    private readonly RegistrationService _registrationService;
    private IValidator<CreateUserDto> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationController"/> class.
    /// </summary>
    /// <param name="registrationService">Service responsible for user registration.</param>
    public RegistrationController(RegistrationService registrationService, IValidator<CreateUserDto> validator)
    {
        _registrationService = registrationService;
        _validator = validator;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="CreateUserDto">User data transfer object containing registration details.</param>
    /// <returns>An IActionResult indicating success or failure of registration.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto createUserDto)
    { 
        var validationResult = _validator.Validate(createUserDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        var result = await _registrationService.Register(createUserDto);
       
        return result.Success ?  Ok(result) : BadRequest(result.Message);
    }

    
    
}