using MediatR;
using OrderService.Dtos.Request;
using OrderService.Models;

namespace OrderService.OrderManagement.Command.CreateOrder;

public record CreateOrderCommand(string UserId, CreateOrderDto Order) : IRequest<Order>;