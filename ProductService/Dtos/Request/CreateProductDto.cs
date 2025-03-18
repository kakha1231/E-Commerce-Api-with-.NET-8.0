using Common.Enums;
using ProductService.Models;

namespace ProductService.Dtos;

public class CreateProductDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public string Category { get; set; }
    
    public bool InStock { get; set; } = true;

    public Product CreateProduct()
    {
        var parsedCategory = Enum.Parse<Category>(Category, true);

        return new Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            Category = parsedCategory,
            InStock = InStock,
        };
    }
    
}