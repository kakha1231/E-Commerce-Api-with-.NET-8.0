using Common.Enums;
using ErrorOr;
using MediatR;
using OrderService.Domain.Agregates;
using OrderService.Domain.Errors;
using OrderService.Infrastructure.Data;

namespace OrderService.Application.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : 
    IRequestHandler<UpdateOrderStatusCommand, ErrorOr<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task< ErrorOr<Order>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderById(request.OrderId);
        
        if (order == null)
        {
            return Errors.OrderNotFound;
        }
        
        var parsedStatus = Enum.Parse<OrderStatus>(request.OrderStatus,true);
        
        order.ChangeStatus(parsedStatus);
        
        await _orderRepository.UpdateOrder(order);
        
        return order;
    }
}