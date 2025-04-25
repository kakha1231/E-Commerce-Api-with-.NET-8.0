using ErrorOr;
using Mapster;
using MediatR;
using ProductService.Domain.Entity;
using ProductService.Domain.Errors;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.Id);

        if (product == null)
        {
            return Errors.ProductNotFound;
        }

         product = request.ProductDto.Adapt(product);

        await _productRepository.UpdateProduct(product);

        return product;
    }
}