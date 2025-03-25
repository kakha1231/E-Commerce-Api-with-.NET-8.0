using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos.Request;
using OrderService.OrderManagement.Command.CreateOrder;
using OrderService.OrderManagement.Query.GetOrderById;

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
        return NoContent();
    }

    [HttpGet("/myorders")]
    public async Task<IActionResult> GetOrdersByUserId(string userId)
    {
        return NoContent();
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
        return NoContent();
        
    }
}