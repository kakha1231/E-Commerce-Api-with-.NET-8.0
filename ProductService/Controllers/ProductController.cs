using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Models;
using ProductService.Services;
using ProductService.Validators;

namespace ProductService.Controllers;


/// <summary>
/// Controller for managing products.
/// Provides endpoints for retrieving, creating, updating, and deleting products.
/// </summary>
[ApiController]
public class ProductController : Controller
{
    private readonly IProductManagementService _productManagementService;
    private readonly IValidator<CreateProductDto> _validator;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="productManagementService">Service for managing product-related operations.</param>
    /// <param name="validator">Validator for validating product creation and update requests.</param>
    public ProductController(IProductManagementService productManagementService, IValidator<CreateProductDto> validator)
    {
        _productManagementService = productManagementService;
        _validator = validator;
    }

    /// <summary>
    /// Retrieves a list of all products.
    /// </summary>
    /// <returns>A list of products.</returns>
    /// <response code="200">Returns the list of products.</response>
    [HttpGet("/products")]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _productManagementService.GetProducts();
        
        return Ok(response.Data);
    }
    
    /// <summary>
    /// Retrieves a specific product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The requested product if found.</returns>
    /// <response code="200">Returns the requested product.</response>
    /// <response code="404">If the product is not found.</response>
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

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="createProductDto">The product data transfer object containing product details.</param>
    /// <returns>The created product.</returns>
    /// <response code="201">Returns the newly created product.</response>
    /// <response code="400">If the request data is invalid.</response>
    [HttpPost("/create-product")]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var validationResult = _validator.Validate(createProductDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        var response = await _productManagementService.CreateProduct(createProductDto);

        return CreatedAtAction(nameof(GetProduct), new { id = response.Data.Id }, response.Data);
    }

    /// <summary>
    /// Updates an existing product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="editProductDto">The updated product details.</param>
    /// <returns>A message indicating the update status.</returns>
    /// <response code="200">Product was successfully updated.</response>
    /// <response code="400">If the request data is invalid.</response>
    /// <response code="404">If the product is not found.</response>
    [HttpPut("/update-product/{id}")]
    public async Task<IActionResult> UpdateProduct(int id,CreateProductDto editProductDto)
    {
        var validationResult = _validator.Validate(editProductDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var response = await _productManagementService.UpdateProduct(id, editProductDto);

        if (!response.Success)
        {
            return NotFound(response.Message);
        }
        
        return Ok(response.Message);
    }

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A message indicating the deletion status.</returns>
    /// <response code="200">Product was successfully deleted.</response>
    /// <response code="404">If the product is not found.</response>
    [HttpDelete("/delete-product/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var response = await _productManagementService.DeleteProduct(id);

        if (!response.Success)
        {
            return NotFound(response.Message);
        }
        
        return Ok(response.Message);
    }
    
    
}