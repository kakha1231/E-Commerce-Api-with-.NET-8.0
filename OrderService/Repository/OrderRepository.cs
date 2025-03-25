using Microsoft.EntityFrameworkCore;
using OrderService.Entity;
using OrderService.Models;

namespace OrderService.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        var order = await _context.Orders.Include(order => order.Items)
            .Include(or => or.Shipping)
            .Where(order => order.Id == orderId).FirstOrDefaultAsync();
        
        if (order == null)
        {
            return null;
        }
        return order;
    }

    public async Task<List<Order>> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        
        return orders;
    }

    public async Task<List<Order>> GetOrdersByUserId(string userId)
    {
        var orders = await _context.Orders.Include(order => order.Items)
            .Include(or => or.Shipping)
            .Where(order => order.UserId == userId).ToListAsync();
        
        return orders;
    }

    public async Task CreateOrder(Order order)
    {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
    }

    public async Task UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
    
}