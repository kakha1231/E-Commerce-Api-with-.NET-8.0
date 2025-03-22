using Common.Enums;

namespace OrderService.Models;

public class Order
{
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public List<OrderItem> Items { get; set; } = [];
    
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    public ShippingInfo Shipping { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; }

}