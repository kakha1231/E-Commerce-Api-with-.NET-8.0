using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductService.Application.Commands.CreateProduct;
using ProductService.Application.Commands.DeleteProduct;
using ProductService.Application.Commands.UpdateProduct;
using ProductService.Application.Dtos.Request;
using ProductService.Application.Queries.GetProductById;
using ProductService.Application.Queries.GetProducts;

namespace ProductService.Presentation.Controllers;


/// <summary>
/// Controller for managing products.
/// Provides endpoints for retrieving, creating, updating, and deleting products.
/// </summary>
[ApiController]
public class ProductController : Controller
{
    private readonly ISender _sender;
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="productManagementService">Service for managing product-related operations.</param>
    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Retrieves a paginated list of products with optional filtering.
    /// </summary>
    /// <param name="category">Filters products by category (optional).</param>
    /// <param name="searchString">Search term to filter products by name or description (optional).</param>
    /// <param name="minPrice">Minimum price filter (default: 0).</param>
    /// <param name="maxPrice">Maximum price filter (default: 9,999,999).</param>
    /// <param name="page">Page number for pagination (default: 1).</param>
    /// <param name="pageSize">Number of items per page (default: 20).</param>
    /// <returns>A paginated list of products that match the filter criteria.</returns>
    /// <response code="200">Returns the filtered and paginated list of products.</response>
    [HttpGet("/products")]
    public async Task<IActionResult> GetProducts([FromQuery] string? category, string? searchString, decimal? minPrice = 0,
        decimal? maxPrice = 9999999, int page = 1, int pageSize = 20)
    {
        var query = new GetProductsQuery(category, searchString, minPrice, maxPrice, page, pageSize);

        var products = await _sender.Send(query);
        
        return Ok(products);
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
        var query = new GetProductByIdQuery(id);

        var result = await _sender.Send(query);

       return result.Match(
            Ok,
            Problem);
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
        var command = new CreateProductCommand(createProductDto);
        
        var result = await _sender.Send(command);

        return result.Match(
            Ok,
            Problem);
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
        var command = new UpdateProductCommand(id, editProductDto);

        var result = await _sender.Send(command);
        
        return result.Match(
            Ok,
            Problem);;
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
        var command = new DeleteProductCommand(id);

        var result = await _sender.Send(command);

        return result.Match(
            Ok,
            Problem);
    }
    
    
    /// <summary>
    /// Converts a list of domain errors into a standardized <see cref="IActionResult"/> response.
    /// </summary>
    /// <param name="errors">A list of <see cref="Error"/> instances representing domain or application-level errors.</param>
    /// <returns>
    /// A standardized <see cref="IActionResult"/> representing the error response,
    /// with an appropriate HTTP status code and error description.
    /// </returns>
    /// <remarks>
    /// Maps error types to the following HTTP status codes:
    /// <list type="bullet">
    ///   <item><description><see cref="ErrorType.NotFound"/> → 404 Not Found</description></item>
    ///   <item><description><see cref="ErrorType.Validation"/> → 400 Bad Request</description></item>
    ///   <item><description><see cref="ErrorType.Conflict"/> → 409 Conflict</description></item>
    ///   <item><description>Any other error → 500 Internal Server Error</description></item>
    /// </list>
    /// </remarks>
    private IActionResult Problem(List<Error> errors)
    {

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var err in errors)
            {
                modelStateDictionary.AddModelError(
                    err.Code,
                    err.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }
        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError 
        };
        
        return Problem(statusCode: statusCode, title: firstError.Description);
    }
    
}