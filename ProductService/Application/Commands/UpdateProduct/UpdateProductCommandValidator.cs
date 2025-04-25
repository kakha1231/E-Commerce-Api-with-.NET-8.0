using Common.Enums;
using FluentValidation;

namespace ProductService.Application.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductDto.Name).NotEmpty()
            .WithMessage("Name is required");
        RuleFor(x => x.ProductDto.Price).NotEmpty()
            .WithMessage("Price is required");
        RuleFor(x => x.ProductDto.Price)
            .Must(x => x is > 0 and <= 1000000)
            .WithMessage("Price must be greater than 0 and less than 1,000,000");
        RuleFor(x => x.ProductDto.Description).NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(512)
            .WithMessage("Description must not exceed 512 characters");
        RuleFor(x => x.ProductDto.Category).NotEmpty()
            .WithMessage("Category is required")
            .Must(x => Enum.IsDefined(typeof(Category), x))
            .WithMessage("Category is not a valid category");
    }
    
}