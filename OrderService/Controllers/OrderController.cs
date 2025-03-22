using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
public class OrderController : Controller
{
    private readonly IOrderManagementService _orderManagementService;

    public OrderController(IOrderManagementService orderManagementService)
    {
        _orderManagementService = orderManagementService;
    }

    [HttpGet("/orders")]
    public async Task<List<Order>> GetOrders()
    {
        return await _orderManagementService.GetOrders();
    }

    [HttpGet("/myorders")]
    public async Task<List<Order>> GetOrdersByUserId(string userId)
    {
        return await _orderManagementService.GetOrdersByUserId(userId);
    }
    
    [HttpGet("/orders/{id}")]
    public async Task<Order> GetOrdersById(int orderId)
    {
        return await _orderManagementService.GetOrdersById(orderId);
    }

    [HttpPost("/create-order")]
    public async Task<Order> CreateOrder(Order order)
    {
        return await _orderManagementService.CreateOrder(order);
    }
    
}