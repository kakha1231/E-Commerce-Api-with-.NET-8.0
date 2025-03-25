using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos.Request;
using OrderService.OrderManagement.Command.CreateOrder;
using OrderService.OrderManagement.Command.UpdateOrderStatus;
using OrderService.OrderManagement.Query.GetOrderById;
using OrderService.OrderManagement.Query.GetOrders;
using OrderService.OrderManagement.Query.GetOrdersByUserId;

namespace OrderService.Controllers;

[ApiController]
public class OrderController : Controller
{
    private readonly ISender _sender;

    public OrderController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/orders")]
    public async Task<IActionResult> GetOrders()
    {
        var query = new GetOrdersQuery();
        
        var orders = await _sender.Send(query);
        
        return Ok(orders);
    }

    [HttpGet("/myorders")]
    public async Task<IActionResult> GetOrdersByUserId(string userId)
    {
        var query = new GetOrdersByUserIdQuery(userId);
        
        var queryResult = await _sender.Send(query);
        
        return Ok(queryResult);
    }
    
    [HttpGet("/orders/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var query = new GetOrderByIdQuery(id);
        
        var order = await _sender.Send(query);

        return Ok(order);
    }

    [HttpPost("/create-order")]
    public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto, string userId)
    {
        var command = new CreateOrderCommand(userId, orderDto);
        
        var order = await _sender.Send(command);
        
        return Ok(order);
    }

    [HttpPut("/update-order/{id}")]
    public async Task<IActionResult> UpdateOrderStatus(string status, int id)
    {
        var command = new UpdateOrderStatusCommand(id, status);
        
        var order = await _sender.Send(command);
        
        return Ok(order);
    }
}