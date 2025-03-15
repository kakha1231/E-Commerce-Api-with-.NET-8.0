using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.Controllers;

[ApiController]
public class ProductController : Controller
{
    private readonly IProductManagementService _productManagementService;

    public ProductController(IProductManagementService productManagementService)
    {
        _productManagementService = productManagementService;
    }

    [HttpGet("/products")]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _productManagementService.GetProducts();
        
        return Ok(response.Data);
    }
    
    [HttpGet("/products/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var response = await _productManagementService.GetProductById(id);

        if (!response.Success)
        {
            return NotFound(response.Message);
        }
        
        return Ok(response.Data);
    }

    [HttpPost("/create-product")]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        var response = await _productManagementService.CreateProduct(product);
        
        return Ok(response.Message);
    }

    [HttpPut("/update-product")]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        var response = await _productManagementService.UpdateProduct(product);
        
        return Ok(response.Message);
    }
}