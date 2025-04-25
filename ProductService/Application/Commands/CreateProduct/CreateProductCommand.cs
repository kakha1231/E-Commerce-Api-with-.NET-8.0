using ErrorOr;
using MediatR;
using ProductService.Application.Dtos.Request;
using ProductService.Domain.Entity;

namespace ProductService.Application.Commands.CreateProduct;


/// <summary>
/// Represents a command to create a new product.
/// </summary>
/// <param name="ProductDto">The DTO containing the product creation data.</param>
/// <returns>
/// Returns an <see cref="ErrorOr{T}"/> that contains the created <see cref="Product"/>
/// if successful, or an error if the operation fails.
/// </returns>
public record CreateProductCommand(CreateProductDto ProductDto) : IRequest<ErrorOr<Product>>;