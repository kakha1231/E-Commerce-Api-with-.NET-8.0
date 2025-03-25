using Common.Enums;
using MediatR;
using OrderService.Models;
using OrderService.Repository;

namespace OrderService.OrderManagement.Command.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : 
    IRequestHandler<UpdateOrderStatusCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderById(request.OrderId);
        
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        
        var parsedStatus = Enum.Parse<OrderStatus>(request.OrderStatus,true);
        
        order.ChangeStatus(parsedStatus);
        
        await _orderRepository.UpdateOrder(order);
        
        return order;
    }
}