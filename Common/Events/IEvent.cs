namespace Common.Events;

public class IEvent
{
    Guid EventId { get; }
    
    DateTime CreatedAt { get; }
}