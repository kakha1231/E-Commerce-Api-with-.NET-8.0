using ErrorOr;
using MediatR;
using ProductService.Application.Dtos.Request;
using ProductService.Domain.Models;

namespace ProductService.Application.Commands.UpdateProduct;

public record UpdateProductCommand(int Id,CreateProductDto ProductDto) : IRequest<ErrorOr<Product>>;