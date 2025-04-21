using Common.Dtos.Response;
using ErrorOr;
using MediatR;
using ProductService.Domain.Entity;
using ProductService.Domain.Errors;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ErrorOr<Product>>
{
    private readonly ProductDbContext _context;

    public GetProductByIdQueryHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id, cancellationToken);

        if (product == null)
        {
            return Errors.ProductNotFound;
        }

        return product;
    }
}