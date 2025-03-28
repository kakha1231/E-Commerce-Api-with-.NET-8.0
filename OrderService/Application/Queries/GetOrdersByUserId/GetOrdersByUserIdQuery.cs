using MediatR;
using OrderService.Domain.Agregates;

namespace OrderService.Application.Queries.GetOrdersByUserId;

public record GetOrdersByUserIdQuery(string userId) : IRequest<List<Order>>;