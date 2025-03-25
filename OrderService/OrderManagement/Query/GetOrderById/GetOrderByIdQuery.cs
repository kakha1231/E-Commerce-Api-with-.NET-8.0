using MediatR;
using OrderService.Models;

namespace OrderService.OrderManagement.Query.GetOrderById;

public record GetOrderByIdQuery(int OrderId) : IRequest<Order>;
