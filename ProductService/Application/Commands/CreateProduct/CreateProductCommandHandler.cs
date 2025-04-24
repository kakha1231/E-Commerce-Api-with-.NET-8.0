using Common.Dtos.Response;
using ErrorOr;
using MediatR;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler( IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = request.ProductDto.CreateProduct();

        await _productRepository.CreateProduct(product);

        return product;
    }
}