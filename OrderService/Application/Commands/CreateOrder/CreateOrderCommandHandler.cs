using Common.Dtos;
using Common.Events;
using Mapster;
using MapsterMapper;
using MediatR;
using OrderService.Application.Dtos.Request;
using OrderService.Domain.Agregates;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.EventStore;
using OrderService.Infrastructure.Messages.Publishers;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderEventPublisher _orderEventPublisher;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, OrderEventPublisher orderEventPublisher, IEventStoreRepository eventStoreRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderEventPublisher = orderEventPublisher;
        _eventStoreRepository = eventStoreRepository;
        _mapper = mapper;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var sourceTuple = (request.Order, request.UserId);
        var order = _mapper.Map<Order>(sourceTuple);

        if (request.Order.Items != null)
        {
            foreach (var orderItem in request.Order.Items)
            {
                order.AddItem(orderItem.ProductId, orderItem.ProductName, orderItem.Quantity, orderItem.UnitPrice);
            }
        }
        
        await _orderRepository.CreateOrder(order);

        var orderCreatedEvent = _mapper.Map<OrderCreatedEvent>(order);
         
        await _eventStoreRepository.SaveEventAsync(orderCreatedEvent, $"order-{order.Id}");
        await _orderEventPublisher.PublishOrderCreatedAsync(orderCreatedEvent);
        
        return order;
    }
}