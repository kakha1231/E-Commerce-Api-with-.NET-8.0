using MediatR;
using OrderService.Models;
using OrderService.Repository;

namespace OrderService.OrderManagement.Query.GetOrders;

public class GetOrdersQueryhandler : IRequestHandler<GetOrdersQuery, List<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryhandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrders();
        return orders;
    }
}