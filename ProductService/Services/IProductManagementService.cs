using Common.Dtos.Response;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.Services;

public interface IProductManagementService
{
    
    
    /// <summary>
    /// Retrieves a list of all products.
    /// </summary>
    /// <returns>A service response containing a list of products.</returns>
    public Task<ServiceResponse<List<Product>>> GetProducts();
    
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