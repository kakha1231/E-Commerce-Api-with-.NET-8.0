using Common.Dtos.Response;
using ErrorOr;
using MediatR;
using ProductService.Domain.Entity;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,ErrorOr<Product>>
{
    private readonly ProductDbContext _context;

    public CreateProductCommandHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = request.ProductDto.CreateProduct();

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }
}