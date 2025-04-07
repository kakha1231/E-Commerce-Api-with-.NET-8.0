using MediatR;
using OrderService.Domain.Agregates;

namespace OrderService.Application.Queries.GetOrders;

public record GetOrdersQuery() : IRequest<List<Order>>;