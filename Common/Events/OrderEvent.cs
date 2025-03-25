using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Events;

public class OrderEvent
{
    Guid Id { get; set; } 
    public int OrderId { get; set; }
    public string EventType { get; set; } 
    public string  Data { get; set; }  

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}