using ErrorOr;
using MediatR;

namespace ProductService.Application.Commands.DeleteProduct;

/// <summary>
/// Represents a command to delete a product by its ID.
/// </summary>
/// <param name="Id">The unique identifier of the product to delete.</param>
/// <returns>
/// Returns an <see cref="ErrorOr{T}"/> containing a success message if the deletion succeeds,
/// or an error if it fails.
/// </returns>
public record DeleteProductCommand(int Id) : IRequest<ErrorOr<string>>;