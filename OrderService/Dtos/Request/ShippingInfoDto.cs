namespace OrderService.Dtos.Request;

public class ShippingInfoDto
{
    public string ContactName { get; set; }
    
    public string Phone { get; set; }
    
    public string Address { get; set; }
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string ZipCode { get; set; }
    
    public string Country { get; set; }
    
    public string Courier { get; set; }
}