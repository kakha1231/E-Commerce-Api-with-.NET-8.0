using Common.Dtos;
using Common.Enums;
using Common.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Dtos.Request;
using OrderService.Entity;
using OrderService.Models;

namespace OrderService.Services;

public class OrderManagementService : IOrderManagementService
{
    private readonly OrderDbContext _orderDbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderManagementService(OrderDbContext orderDbContext, IPublishEndpoint publishEndpoint)
    {
        _orderDbContext = orderDbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<List<Order>> GetOrders()
    {
        var orders = _orderDbContext.Orders.Include(order => order.Items)
            .Include(or => or.Shipping ).ToListAsync();
        
        return await orders;
    }

    public async Task<List<Order>> GetOrdersByUserId(string userId)
    {
        var orders = await _orderDbContext.Orders.Include(order => order.Items)
            .Include(or => or.Shipping).Where(order => order.UserId == userId).ToListAsync();
        
        return orders;
    }

    public async Task<Order> GetOrdersById(int orderId)
    {
        var order = await _orderDbContext.Orders.Include(order => order.Items)
            .Include(or => or.Shipping).Where(order => order.Id == orderId).FirstOrDefaultAsync();
        
        return order;
    }


    public async Task<Order> CreateOrder(CreateOrderDto orderDto, string userId)
    {
        var order = new Order
        {
            UserId = userId,
            Items = orderDto.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
                }).ToList(),
            Status = OrderStatus.Pending,
            Shipping = new ShippingInfo()
            {
                ContactName = orderDto.Shipping.ContactName,
                Phone = orderDto.Shipping.Phone,
                Address = orderDto.Shipping.Address,
                City = orderDto.Shipping.City,
                State = orderDto.Shipping.State,
                ZipCode = orderDto.Shipping.ZipCode,
                Country = orderDto.Shipping.Country,
                Courier = orderDto.Shipping.Courier,
            },
            CreatedAt = DateTime.UtcNow,
        };
        
        
        await _orderDbContext.Orders.AddAsync(order);
        await _orderDbContext.SaveChangesAsync();

        await _publishEndpoint.Publish(new OrderCreatedEvent
        {
            OrderId = order.Id,
            UserId = order.UserId,
            Status = order.Status,
            Items = order.Items.Select(it => new OrderItemEventDto()
            {
                Productid = it.ProductId,
                ProductName = it.ProductName, 
                Quantity = it.Quantity,
            }).ToList(),
        });
        
        return order;
    }
}