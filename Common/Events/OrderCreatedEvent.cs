using Common.Dtos;
using Common.Enums;
using OrderService.Application.Dtos.Request;

namespace Common.Events;

public class OrderCreatedEvent : IEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    
    public int OrderId { get; set; }
    
    public string UserId { get; set; }
    
    public OrderStatus Status { get; set; }
   
    public List<OrderItemDto> Items { get; set; } = [];
    
}