using Microsoft.EntityFrameworkCore;
using OrderService.Entity;
using OrderService.Models;

namespace OrderService.Services;

public class OrderManagementService : IOrderManagementService
{
    private readonly OrderDbContext _orderDbContext;

    public OrderManagementService(OrderDbContext orderDbContext)
    {
        _orderDbContext = orderDbContext;
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


    public async Task<Order> CreateOrder(Order order)
    {
        await _orderDbContext.Orders.AddAsync(order);
        await _orderDbContext.SaveChangesAsync();
        
        return order;
    }
}