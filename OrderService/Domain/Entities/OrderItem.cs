using OrderService.Domain.Agregates;

namespace OrderService.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; } 
    public int ProductId { get; set; } 
    public string ProductName { get; set; } 
    public int Quantity { get; set; } 
    public decimal UnitPrice { get; set; } 
    public decimal TotalPrice => Quantity * UnitPrice; 
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    private OrderItem() { }

    public OrderItem(int productId, string productName, int quantity, decimal unitPrice, Order order)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Order = order;
        OrderId = order.Id; // Set OrderId
    }
}