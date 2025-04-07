using FluentValidation;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Order.Shipping.Address).NotEmpty()
            .WithMessage("Address must not be empty");
        RuleFor(x => x.Order.Shipping.City).NotEmpty()
            .WithMessage("City must not be empty");
        RuleFor(x => x.Order.Shipping.ContactName).NotEmpty()
            .WithMessage("Contact Name must not be empty");
        RuleFor(x => x.Order.Shipping.Country).NotEmpty()
            .WithMessage("Country Name must not be empty");
        RuleFor(x => x.Order.Shipping.Courier).NotEmpty()
            .WithMessage("Courier Name must not be empty");
        RuleFor(x => x.Order.Shipping.Phone).NotEmpty()
            .WithMessage("Phone Number Name must not be empty");
        RuleFor(x => x.Order.Shipping.State).NotEmpty()
            .WithMessage("State Name must not be empty");
        RuleFor(x => x.Order.Shipping.ZipCode).NotEmpty()
            .WithMessage("Zip Code Name must not be empty");
        RuleForEach(x => x.Order.Items).NotEmpty().NotNull()
            .WithMessage("Items must not be empty");
    }
    
}