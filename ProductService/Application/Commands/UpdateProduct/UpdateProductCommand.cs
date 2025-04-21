using ErrorOr;
using MediatR;
using ProductService.Application.Dtos.Request;
using ProductService.Domain.Entity;

namespace ProductService.Application.Commands.UpdateProduct;

/// <summary>
/// Represents a command to update an existing product.
/// </summary>
/// <param name="Id">The unique identifier of the product to update.</param>
/// <param name="ProductDto">The DTO containing the updated product data.</param>
/// <returns>
/// Returns an <see cref="ErrorOr{T}"/> containing the updated <see cref="Product"/> if successful,
/// or an error if the update fails.
/// </returns>
public record UpdateProductCommand(int Id,CreateProductDto ProductDto) : IRequest<ErrorOr<Product>>;