using OrderService.Dtos.Request;
using OrderService.Models;

namespace OrderService.Services;

public interface IOrderManagementService
{
    public Task<List<Order>> GetOrders();
    public Task<List<Order>> GetOrdersByUserId(string userId);
    public Task<Order> GetOrdersById(int orderId);
    public Task<Order> CreateOrder(CreateOrderDto createOrderDto,string userId);
}