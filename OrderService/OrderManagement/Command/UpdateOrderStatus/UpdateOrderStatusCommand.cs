using Common.Enums;
using MediatR;
using OrderService.Models;

namespace OrderService.OrderManagement.Command.UpdateOrderStatus;

public record UpdateOrderStatusCommand(int OrderId, string OrderStatus) : IRequest<Order>;