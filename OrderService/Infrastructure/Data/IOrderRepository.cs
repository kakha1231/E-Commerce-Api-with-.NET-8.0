using OrderService.Domain.Agregates;

namespace OrderService.Infrastructure.Data;

public interface IOrderRepository
{
    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <returns>The order if found; otherwise, null.</returns>
    public Task<Order?> GetOrderById(int orderId);
    
    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    public Task<List<Order>> GetOrders();
    
    /// <summary>
    /// Retrieves all orders associated with a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A list of orders placed by the specified user.</returns>
    public Task<List<Order>> GetOrdersByUserId(string userId);
    
    /// <summary>
    /// Creates a new order and saves it to the database.
    /// </summary>
    /// <param name="order">The order to be created.</param>
    public Task CreateOrder(Order order);
    
    /// <summary>
    /// Updates an existing order in the database.
    /// </summary>
    /// <param name="order">The order with updated information.</param>
    public Task UpdateOrder(Order order);

}