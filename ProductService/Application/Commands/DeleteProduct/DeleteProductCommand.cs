using ErrorOr;
using MediatR;

namespace ProductService.Application.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<ErrorOr<string>>;