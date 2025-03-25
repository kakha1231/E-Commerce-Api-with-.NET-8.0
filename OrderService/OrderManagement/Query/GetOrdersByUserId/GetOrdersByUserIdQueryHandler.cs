

using MediatR;
using OrderService.Models;
using OrderService.Repository;

namespace OrderService.OrderManagement.Query.GetOrdersByUserId;

public class GetOrdersByUserIdQueryHandler :IRequestHandler<GetOrdersByUserIdQuery, List<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<Order>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByUserId(request.userId);
        
        return orders;
    }
}