using MediatR;
using OrderService.Models;

namespace OrderService.OrderManagement.Query.GetOrders;

public record GetOrdersQuery() : IRequest<List<Order>>;