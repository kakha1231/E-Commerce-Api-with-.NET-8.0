using MediatR;
using OrderService.Domain.Agregates;

namespace OrderService.Application.Queries.GetOrderById;

public record GetOrderByIdQuery(int OrderId) : IRequest<Order>;
