using System.Data;
using Common.Enums;
using FluentValidation;
using ProductService.Dtos;

namespace ProductService.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(256).WithMessage("Description must not exceed 256 characters");
        
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .Must(x => Enum.IsDefined(typeof(Category), x)).WithMessage("Category is not a valid category");
            
    }
    
}