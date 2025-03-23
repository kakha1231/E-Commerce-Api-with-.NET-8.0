using Common.Dtos;
using Common.Events;
using MassTransit;
using MongoDB.Driver;
using OrderService.Models;

namespace OrderService.Consumers;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IMongoCollection<OrderReadModel> _orders;

    public OrderCreatedEventConsumer(IMongoDatabase database)
    {
        _orders = database.GetCollection<OrderReadModel>("Orders");
    }
    
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {

        var order = new OrderReadModel
        {
            OrderId = context.Message.OrderId,
            Items = context.Message.Items.Select(it => new OrderItemEventDto
            {
                Productid = it.Productid,
                ProductName = it.ProductName,
                Quantity = it.Quantity,
            }).ToList()
        };

        await _orders.InsertOneAsync(order);
        
        Console.WriteLine("order readmodel added");
    }
    
}