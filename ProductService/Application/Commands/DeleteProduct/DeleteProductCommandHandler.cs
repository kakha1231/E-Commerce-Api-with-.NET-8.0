using Common.Dtos.Response;
using ErrorOr;
using MediatR;
using ProductService.Domain.Errors;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand,ErrorOr<string>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler( IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.Id);
      
        if (product == null)
        {
            return Errors.ProductNotFound;
        }

        await _productRepository.DeleteProduct(product);

        return "Product Deleted";
    }
}