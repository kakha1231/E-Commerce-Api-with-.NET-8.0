using OrderService.Models;

namespace OrderService.Repository;

public interface IOrderRepository
{
    public Task<Order> GetOrderById(int orderId);
    public Task<List<Order>> GetOrders();
    public Task<List<Order>> GetOrdersByUserId(string userId);
    public Task CreateOrder(Order order);
    public Task UpdateOrder(Order order);

}