using Common.Dtos;
using Common.Enums;

namespace OrderService.Models;

public class OrderReadModel
{
    public int OrderId { get; set; }
    
    public string UserId { get; set; }
    
    public List<OrderItemEventDto> Items { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}