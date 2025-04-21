using ErrorOr;
using MediatR;
using ProductService.Application.Dtos.Request;
using ProductService.Domain.Models;

namespace ProductService.Application.Commands.CreateProduct;

public record CreateProductCommand(CreateProductDto ProductDto) : IRequest<ErrorOr<Product>>;