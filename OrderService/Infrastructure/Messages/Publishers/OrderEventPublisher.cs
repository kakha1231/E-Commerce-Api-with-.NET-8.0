using Common.Events;
using MassTransit;

namespace OrderService.Infrastructure.Messages.Publishers;

public class OrderEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishOrderCreatedAsync(OrderCreatedEvent orderCreated)
    {
        await _publishEndpoint.Publish(orderCreated);
    }

    public async Task PublishOrderUpdatedAsync(OrderUpdatedEvent orderUpdated)
    {
        await _publishEndpoint.Publish(orderUpdated);
    }
}