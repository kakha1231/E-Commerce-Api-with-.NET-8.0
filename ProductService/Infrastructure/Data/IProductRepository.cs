using ProductService.Domain.Entity;

namespace ProductService.Infrastructure.Data;

/// <summary>
/// Defines the contract for interacting with product data in the repository.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <returns>The product if found; otherwise, null.</returns>
    public Task<Product?> GetProductById(int id);

    /// <summary>
    /// Retrieves a filtered and paginated list of products.
    /// </summary>
    /// <param name="category">The category to filter by (optional).</param>
    /// <param name="searchString">A search keyword to filter product names or descriptions (optional).</param>
    /// <param name="minPrice">The minimum price to filter by (optional).</param>
    /// <param name="maxPrice">The maximum price to filter by (optional).</param>
    /// <param name="page">The page number for pagination (1-based).</param>
    /// <param name="pageSize">The number of products per page.</param>
    /// <returns>A list of products matching the specified filters.</returns>
    public Task<List<Product>> GetProducts(
        string? category,
        string? searchString,
        decimal? minPrice,
        decimal? maxPrice,
        int page,
        int pageSize);

    /// <summary>
    /// Adds a new product to the repository.
    /// </summary>
    /// <param name="product">The product to create.</param>
    public Task CreateProduct(Product product);

    
    /// <summary>
    /// Updates an existing product in the repository.
    /// </summary>
    /// <param name="product">The product to update.</param>
    public Task UpdateProduct(Product product);

    
    /// <summary>
    /// Deletes a product from the repository.
    /// </summary>
    /// <param name="product">The product to delete.</param>
    public Task DeleteProduct(Product product);
}