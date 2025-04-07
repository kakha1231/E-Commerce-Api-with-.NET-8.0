using ErrorOr;
using MediatR;
using OrderService.Domain.Agregates;

namespace OrderService.Application.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(int OrderId, string OrderStatus) : IRequest<ErrorOr<Order>>;