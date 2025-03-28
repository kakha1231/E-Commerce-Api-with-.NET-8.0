using OrderService.Domain.Agregates;

namespace OrderService.Domain.ValueObjects;

public class ShippingInfo
{
    public int Id { get; set; }
    public string ContactName { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }
    public string Country { get; private set; }
    public string Courier { get; private set; }
    
    public int OrderId { get; private set; }
    public Order Order { get; private set; }
    
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
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ShippingInfo other = (ShippingInfo)obj;
        return ContactName == other.ContactName &&
               Phone == other.Phone &&
               Address == other.Address &&
               City == other.City &&
               State == other.State &&
               ZipCode == other.ZipCode &&
               Country == other.Country &&
               Courier == other.Courier;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(ContactName, Phone, Address, City, State, ZipCode, Country, Courier);
    }

}