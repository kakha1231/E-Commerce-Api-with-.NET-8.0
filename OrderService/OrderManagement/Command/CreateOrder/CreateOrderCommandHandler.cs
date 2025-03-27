using Common.Dtos;
using Common.Events;
using MediatR;
using OrderService.EventStore;
using OrderService.Messages.Publishers;
using OrderService.Models;
using OrderService.Repository;

namespace OrderService.OrderManagement.Command.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderEventPublisher _orderEventPublisher;
    private readonly IEventStoreRepository _eventStoreRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, OrderEventPublisher orderEventPublisher, IEventStoreRepository eventStoreRepository)
    {
        _orderRepository = orderRepository;
        _orderEventPublisher = orderEventPublisher;
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.UserId, request.Order.Shipping, request.Order.Items);
        
        await _orderRepository.CreateOrder(order);

        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            UserId = order.UserId,
            Status = order.Status,
            Items = order.Items.Select(it => new OrderItemSnapshot()
            {
                Productid = it.ProductId,
                ProductName = it.ProductName,
                Quantity = it.Quantity,
            }).ToList(),
        };
         
        await _eventStoreRepository.SaveEventAsync(orderCreatedEvent, $"order-{order.Id}");
        await _orderEventPublisher.PublishOrderCreatedAsync(orderCreatedEvent);
        
        return order;
    }
}