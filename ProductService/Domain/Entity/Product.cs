using Common.Enums;

namespace ProductService.Domain.Entity;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public Category Category { get; set; }
    
    public bool InStock { get; set; }
}