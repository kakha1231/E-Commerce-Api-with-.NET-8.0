using ErrorOr;
using MediatR;
using ProductService.Domain.Entity;

namespace ProductService.Application.Queries.GetProducts;


/// <summary>
/// Represents a query for retrieving a filtered and paginated list of products.
/// </summary>
/// <param name="Category">Filters products by category (optional).</param>
/// <param name="SearchString">Search term to filter products by name or description (optional).</param>
/// <param name="MinPrice">Minimum price filter (default: 0).</param>
/// <param name="MaxPrice">Maximum price filter (default: 9,999,999).</param>
/// <param name="Page">Page number for pagination (default: 1).</param>
/// <param name="PageSize">Number of items per page (default: 20).</param>
/// <returns>A service response containing a paginated list of products.</returns>
public record GetProductsQuery(string? Category, string? SearchString, decimal? MinPrice = 0,
    decimal? MaxPrice = 9999999, int Page = 1, int PageSize = 20 ) : IRequest<List<Product>>
{
    
}
