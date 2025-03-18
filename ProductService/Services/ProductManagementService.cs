﻿using Common.Dtos.Response;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using ProductService.Dtos;
using ProductService.Entity;
using ProductService.Models;

namespace ProductService.Services;

public class ProductManagementService : IProductManagementService
{
    private readonly ProductDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductManagementService"/> class.
    /// </summary>
    /// <param name="context">Database context for product operations.</param>
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
    
    public async Task<ServiceResponse<Product>> CreateProduct(CreateProductDto createProductDto)
    {
        var product = createProductDto.CreateProduct();

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        return new ServiceResponse<Product>
        {
            Success = true,
            Message = "Product created",
            Data = product
        };
    }
    
    public async Task<ServiceResponse<Product>> UpdateProduct(int id,CreateProductDto enProductDto)
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
        
        product.Name = enProductDto.Name;
        product.Description = enProductDto.Description;
        product.Price = enProductDto.Price;
        product.Category = Enum.Parse<Category>(enProductDto.Category, true);
        product.InStock = enProductDto.InStock;
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        
        return new ServiceResponse<Product>
        {
            Success = true,
            Message = "Product updated",
            Data = product
        };
    }
    
    public async Task<ServiceResponse<Product>> DeleteProduct(int id)
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
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return new ServiceResponse<Product>
        {
            Success = true,
            Message = "Product deleted",
            Data = null,
        };
    }
    
    
    
}