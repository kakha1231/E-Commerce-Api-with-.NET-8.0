using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Commands.UpdateOrderStatus;
using OrderService.Application.Dtos.Request;
using OrderService.Application.Queries.GetOrderById;
using OrderService.Application.Queries.GetOrders;
using OrderService.Application.Queries.GetOrdersByUserId;

namespace OrderService.Presentation.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly ISender _sender;

    public OrderController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Retrieves a list of all orders.
    /// </summary>
    /// <returns>A list of orders.</returns>
    /// <response code="200">Returns the list of orders.</response>
    [HttpGet("/orders")]
    public async Task<IActionResult> GetOrders()
    {
        var query = new GetOrdersQuery();
        
        var orders = await _sender.Send(query);
        
        return Ok(orders);
    }

    /// <summary>
    /// Retrieves a list of orders by user ID.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>A list of orders belonging to the specified user.</returns>
    /// <response code="200">Returns the list of user-specific orders.</response>
    [HttpGet("/myorders")]
    public async Task<IActionResult> GetOrdersByUserId(string userId)
    {
        var query = new GetOrdersByUserIdQuery(userId);
        
        var queryResult = await _sender.Send(query);
        
        return Ok(queryResult);
    }
    
    /// <summary>
    /// Retrieves a specific order by its ID.
    /// </summary>
    /// <param name="id">The order ID.</param>
    /// <returns>The requested order.</returns>
    /// <response code="200">Returns the requested order.</response>
    /// <response code="404">Order not found.</response>
    [HttpGet("/orders/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var query = new GetOrderByIdQuery(id);
        
        var result = await _sender.Send(query);

        return result.Match(
            order => Ok(order),
            Problem);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="orderDto">The order data transfer object.</param>
    /// <param name="userId">The unique identifier of the user creating the order.</param>
    /// <returns>The newly created order.</returns>
    /// <response code="200">Successfully creates and returns the new order.</response>
    /// <response code="400">If the request is invalid.</response>
    [HttpPost("/create-order")]
    public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto, string userId)
    {
        var command = new CreateOrderCommand(userId, orderDto);
        
        var result = await _sender.Send(command);
        
        return result.Match(
            order => Ok(order),
            Problem);
    }

    
    /// <summary>
    /// Updates the status of an order.
    /// </summary>
    /// <param name="status">The new status for the order (e.g., "Pending", "Shipped").</param>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>The updated order.</returns>
    /// <response code="200">Successfully updates and returns the order.</response>
    /// <response code="404">If the order with the given ID is not found.</response>
    /// <response code="400">If the status provided is invalid.</response>
    [HttpPut("/update-order/{id}")]
    public async Task<IActionResult> UpdateOrderStatus(string status, int id)
    {
        var command = new UpdateOrderStatusCommand(id, status);
        
        var result = await _sender.Send(command);
        
        return result.Match(
            order => Ok(order),
            Problem);;
    }
    
    
    /// <summary>
    /// Converts a list of domain errors into a standardized <see cref="IActionResult"/> response.
    /// </summary>
    /// <param name="errors">A list of <see cref="Error"/> instances representing domain or application-level errors.</param>
    /// <returns>
    /// A standardized <see cref="IActionResult"/> representing the error response,
    /// with an appropriate HTTP status code and error description.
    /// </returns>
    /// <remarks>
    /// Maps error types to the following HTTP status codes:
    /// <list type="bullet">
    ///   <item><description><see cref="ErrorType.NotFound"/> → 404 Not Found</description></item>
    ///   <item><description><see cref="ErrorType.Validation"/> → 400 Bad Request</description></item>
    ///   <item><description><see cref="ErrorType.Conflict"/> → 409 Conflict</description></item>
    ///   <item><description>Any other error → 500 Internal Server Error</description></item>
    /// </list>
    /// </remarks>
    private IActionResult Problem(List<Error> errors)
    {

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var err in errors)
            {
                modelStateDictionary.AddModelError(
                    err.Code,
                    err.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }
        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError 
        };
        
        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}