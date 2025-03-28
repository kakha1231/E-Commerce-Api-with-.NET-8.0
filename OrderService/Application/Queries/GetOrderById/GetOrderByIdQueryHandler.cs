using MediatR;
using OrderService.Domain.Agregates;
using OrderService.Infrastructure.Data;

namespace OrderService.Application.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
{
    
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
       var order = await _orderRepository.GetOrderById(request.OrderId);
       
       return order;
    }
}