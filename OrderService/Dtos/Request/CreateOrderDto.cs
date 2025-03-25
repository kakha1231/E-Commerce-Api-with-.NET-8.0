using OrderService.Models;

namespace OrderService.Dtos.Request;

public class CreateOrderDto
{
    public List<CreateOrderItemDto> Items { get; set; } = [];
    
    public ShippingInfoDto Shipping { get; set; }
}