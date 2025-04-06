using ErrorOr;
using MediatR;
using OrderService.Application.Dtos.Request;
using OrderService.Domain.Agregates;

namespace OrderService.Application.Commands.CreateOrder;

public record CreateOrderCommand(string UserId, CreateOrderDto Order) : IRequest<ErrorOr<Order>>;