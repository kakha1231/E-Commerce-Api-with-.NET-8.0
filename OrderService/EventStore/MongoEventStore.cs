using System.Text.Json;
using Common.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace OrderService.EventStore;

public class MongoEventStore
{
    private readonly IMongoCollection<OrderEvent> _events;

    public MongoEventStore(IMongoDatabase database)
    {
        _events = database.GetCollection<OrderEvent>("OrderEvents");
    }

    public async Task SaveEventAsync(int orderId, string eventType, object data)
    {
        
        var orderEvent = new OrderEvent
        {
            OrderId = orderId,
            EventType = eventType, 
            Data = JsonSerializer.Serialize(data),
        };
        
        await _events.InsertOneAsync(orderEvent);
    }

    public async Task<List<OrderEvent>> GetEventsAsync(int orderId)
    {
        return await _events.Find(e => e.OrderId == orderId).ToListAsync();
    }
}
