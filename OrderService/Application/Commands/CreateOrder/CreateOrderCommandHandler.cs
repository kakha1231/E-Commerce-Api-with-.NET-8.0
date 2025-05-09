﻿using Common.Events;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using OrderService.Domain.Agregates;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Messages.Publishers;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderEventPublisher _orderEventPublisher;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, OrderEventPublisher orderEventPublisher, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderEventPublisher = orderEventPublisher;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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
        
        await _orderEventPublisher.PublishOrderCreatedAsync(orderCreatedEvent);
        
        return order;
    }
}