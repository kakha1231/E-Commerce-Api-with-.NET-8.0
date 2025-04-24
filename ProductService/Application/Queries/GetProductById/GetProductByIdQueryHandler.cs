using ErrorOr;
using MediatR;
using ProductService.Domain.Entity;
using ProductService.Domain.Errors;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler( IProductRepository productRepository)
    {
        _productRepository = productRepository;
        
    }

    public async Task<ErrorOr<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.Id);

        if (product == null)
        {
            return Errors.ProductNotFound;
        }
        
        return product;
    }
}