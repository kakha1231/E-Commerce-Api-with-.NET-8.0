using Common.Enums;
using FluentValidation;

namespace OrderService.Application.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.OrderStatus)
            .Must(x => Enum.IsDefined(typeof(OrderStatus), x)).WithMessage("Status is not a valid");
    }
    
}