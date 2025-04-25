using MediatR;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery,List<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler( IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProducts(
            category: request.Category,
            searchString: request.SearchString,
            minPrice: request.MinPrice,
            maxPrice: request.MaxPrice,
            page: request.Page,
            pageSize: request.PageSize);
        
        return products;
    }
}