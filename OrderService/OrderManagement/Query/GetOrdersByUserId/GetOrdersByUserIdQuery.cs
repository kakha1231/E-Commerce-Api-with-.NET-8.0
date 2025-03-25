using MediatR;
using OrderService.Models;

namespace OrderService.OrderManagement.Query.GetOrdersByUserId;

public record GetOrdersByUserIdQuery(string userId) : IRequest<List<Order>>;