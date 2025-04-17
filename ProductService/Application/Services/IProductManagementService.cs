using Common.Dtos.Response;
using ProductService.Application.Dtos.Request;
using ProductService.Domain.Models;

namespace ProductService.Application.Services;

public interface IProductManagementService
{


    /// <summary>
    /// Retrieves a paginated list of products with optional filtering.
    /// </summary>
    /// <param name="category">Filters products by category (optional).</param>
    /// <param name="searchString">Search term to filter products by name or description (optional).</param>
    /// <param name="minPrice">Minimum price filter (default: 0).</param>
    /// <param name="maxPrice">Maximum price filter (default: 9,999,999).</param>
    /// <param name="page">Page number for pagination (default: 1).</param>
    /// <param name="pageSize">Number of items per page (default: 20).</param>
    /// <returns>A service response containing a paginated list of products.</returns>
    public Task<ServiceResponse<List<Product>>> GetProducts(string? category, string? searchString,
        decimal? minPrice = 0,
        decimal? maxPrice = 9999999, int page = 1, int pageSize = 20);
    
    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The product ID.</param>
    /// <returns>A service response containing the product if found; otherwise, an error message.</returns>
    public Task<ServiceResponse<Product>> GetProductById(int productId);
    
    /// <summary>
    /// Creates a new product and saves it to the database.
    /// </summary>
    /// <param name="createProductDto">Data transfer object containing product details.</param>
    /// <returns>A service response indicating success or failure, along with the created product.</returns>
    public Task<ServiceResponse<Product>> CreateProduct(CreateProductDto createProductDto);
    
    /// <summary>
    /// Updates an existing product in the database.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="enProductDto">Data transfer object containing updated product details.</param>
    /// <returns>A service response indicating success or failure, along with the updated product.</returns>
    public Task<ServiceResponse<Product>> UpdateProduct(int id,CreateProductDto editProductDto);
   
    /// <summary>
    /// Deletes a product from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A service response indicating success or failure.</returns>
    public Task<ServiceResponse<Product>> DeleteProduct(int id);
}