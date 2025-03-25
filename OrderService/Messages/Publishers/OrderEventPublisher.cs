using Common.Events;
using MassTransit;
using OrderService.EventStore;

namespace OrderService.Messages.Publishers;

public class OrderEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly MongoEventStore _eventStore;

    public OrderEventPublisher(IPublishEndpoint publishEndpoint,  MongoEventStore eventStore)
    {
        _publishEndpoint = publishEndpoint;
        _eventStore = eventStore;
    }

    public async Task PublishOrderCreatedAsync(OrderCreatedEvent orderCreated)
    {
        await _publishEndpoint.Publish(orderCreated);
       
        await _eventStore.SaveEventAsync(orderCreated.OrderId, "OrderCreated", orderCreated);
    }

    public async Task PublishOrderUpdatedAsync(OrderUpdatedEvent orderUpdated)
    {
        await _publishEndpoint.Publish(orderUpdated);
        
        await _eventStore.SaveEventAsync(orderUpdated.OrderId, "OrderUpdated", orderUpdated);
    }
}