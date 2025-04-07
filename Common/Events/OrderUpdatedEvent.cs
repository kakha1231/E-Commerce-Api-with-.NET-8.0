using Common.Enums;

namespace Common.Events;

public class OrderUpdatedEvent : IEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    
    public int OrderId { get; set; }
    
    public OrderStatus Status { get; set; }
}