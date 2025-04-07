using Common.Enums;
using OrderService.Application.Dtos.Request;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Domain.Agregates;

public class Order
{
    public int Id { get; private set; }
    public string UserId { get; private set; }
    
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public ShippingInfo Shipping { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }
    
    private Order() {}

    public Order(string userId, string contactName, string phone, string address, 
        string city, string state, string zipCode, string country, string courier)
    {
        UserId = userId;
        Shipping = new ShippingInfo(contactName, phone, address, city, state, zipCode, country, courier);
    }

    public void AddItem(int productId, string productName, int quantity, decimal unitPrice)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        var existingItem = _items.FirstOrDefault(item => item.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _items.Add(new OrderItem(productId, productName, quantity, unitPrice, this));
        }
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void ChangeStatus(OrderStatus newStatus)
    {
        if (newStatus == OrderStatus.Cancelled && Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Cannot cancel a completed order.");

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}