using System.Security.AccessControl;
using System.Text.RegularExpressions;
using AuthorizationService.Dtos.Request;
using FluentValidation;

namespace AuthorizationService.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty")
            .Must(firstname => firstname.All(Char.IsAsciiLetter)).WithMessage("First name must only contain letters and spaces")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long")
            .MaximumLength(20).WithMessage("First name must be between 2 and 20 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty")
            .Must(firstname => firstname.All(Char.IsAsciiLetter)).WithMessage("Last name must only contain letters and spaces")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long")
            .MaximumLength(20).WithMessage("Last name must be between 2 and 20 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .MaximumLength(20).WithMessage("Password must be between 8 and 20 characters")
            .Must(password => password.Any(char.IsUpper))
            .WithMessage("Password must contain at least one upper case letter")
            .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain at least one digit")
            .Must(password => Regex.IsMatch(password, @"[\W_]"))
            .WithMessage("Password must contain at least one non-alphanumeric character");
    }
}