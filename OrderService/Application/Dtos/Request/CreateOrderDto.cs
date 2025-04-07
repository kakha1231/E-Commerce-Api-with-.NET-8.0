namespace OrderService.Application.Dtos.Request;

public class CreateOrderDto
{
    public List<OrderItemDto> Items { get; set; } = [];
    
    public ShippingInfoDto Shipping { get; set; }
}