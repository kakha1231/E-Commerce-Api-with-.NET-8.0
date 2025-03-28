using Common.Events;
using Mapster;
using OrderService.Application.Dtos.Request;
using OrderService.Domain.Agregates;

namespace OrderService.Application.Mapping;

public class MapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateOrderDto, string), Order>()
            .ConstructUsing(src => new Order(
                src.Item2,
                src.Item1.Shipping.ContactName,
                src.Item1.Shipping.Phone,
                src.Item1.Shipping.Address,
                src.Item1.Shipping.City,
                src.Item1.Shipping.State,
                src.Item1.Shipping.ZipCode,
                src.Item1.Shipping.Country,
                src.Item1.Shipping.Courier
            ))
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Items)
            .Ignore(dest => dest.Status)
            .Ignore(dest => dest.CreatedAt)
            .Ignore(dest => dest.UpdatedAt);
        
        config.NewConfig<Order, OrderCreatedEvent>()
            .Map(dest => dest.OrderId, src => src.Id)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Items, src => src.Items.Adapt<List<OrderItemDto>>());
    }
}