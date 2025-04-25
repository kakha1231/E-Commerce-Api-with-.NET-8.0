using ErrorOr;
using MediatR;
using ProductService.Domain.Entity;

namespace ProductService.Application.Queries.GetProductById;

/// <summary>
/// Represents a query to retrieve a product by its ID.
/// </summary>
/// <param name="Id">The unique identifier of the product to retrieve.</param>
/// <returns>
/// Returns an <see cref="ErrorOr{T}"/> containing the <see cref="Product"/> if found,
/// or an error if the product does not exist.
/// </returns>
public record GetProductByIdQuery(int Id) : IRequest<ErrorOr<Product>>;