using Common.Enums;
using OrderService.Dtos.Request;

namespace OrderService.Models;

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

    public Order(string userId, ShippingInfoDto shippingDto, List<CreateOrderItemDto> items)
    {
        UserId = userId;
        Shipping = new ShippingInfo(
            shippingDto.ContactName,
            shippingDto.Phone,
            shippingDto.Address,
            shippingDto.City,
            shippingDto.State,
            shippingDto.ZipCode,
            shippingDto.Country,
            shippingDto.Courier
        );

        foreach (var item in items)
        {
            AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
        }
    }

    private void AddItem(int productId, string productName, int quantity, decimal unitPrice)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
        _items.Add(new OrderItem(productId, productName, quantity, unitPrice));
    }
    
    public void ChangeStatus(OrderStatus newStatus)
    {
        if (newStatus == OrderStatus.Cancelled && Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Cannot cancel a completed order.");

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}