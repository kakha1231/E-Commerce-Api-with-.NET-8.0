using Common.Dtos.Response;
using Microsoft.EntityFrameworkCore;
using ProductService.Entity;
using ProductService.Models;

namespace ProductService.Services;

public class ProductManagementService : IProductManagementService
{
    private readonly ProductDbContext _context;

    public ProductManagementService(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Product>>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();

        return new ServiceResponse<List<Product>>
        {
            Success = true,
            Data = products
        };
    }

    public async Task<ServiceResponse<Product>> GetProductById(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return new ServiceResponse<Product>
            {
                Success = false,
                Message = "Product not found",
                Data = null
            };
        }
        return new ServiceResponse<Product>
        {
            Success = true,
            Data = product
        };
    }

    public async Task<ServiceResponse<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        return new ServiceResponse<Product>
        {
            Success = true,
            Message = "Product created",
            Data = product
        };
    }

    public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        
        return new ServiceResponse<Product>
        {
            Success = true,
            Message = "Product updated",
            Data = product
        };
    }
    
    
}