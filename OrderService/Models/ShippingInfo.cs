namespace OrderService.Models;

public class ShippingInfo
{
    public int Id { get; set; } 
    
    public string ContactName { get; set; }
    
    public string Phone { get; set; }
    
    public string Address { get; set; }
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string ZipCode { get; set; }
    
    public string Country { get; set; }
    
    public string Courier { get; set; } // e.g., "FedEx", "DHL"
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    private ShippingInfo() { }
    
    public ShippingInfo(
        string contactName,
        string phone,
        string address,
        string city,
        string state,
        string zipCode,
        string country,
        string courier)
    {
        ContactName = contactName;
        Phone = phone;
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        Courier = courier;
    }
}