using Common.Enums;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery,List<Product>>
{
    private readonly ProductDbContext _context;

    public GetProductsQueryHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            query = query.Where(p => p.Name.Contains(request.SearchString));
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            var parsedCategory = Enum.Parse<Category>(request.Category,true);
            
            query = query.Where(p => p.Category == parsedCategory);
        }
        
        query = query.Where(p => p.Price >= request.MinPrice && p.Price <= request.MaxPrice && p.InStock);
        
        var products = await query
            .OrderBy(p => p.Id) 
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        return products;
    }
}