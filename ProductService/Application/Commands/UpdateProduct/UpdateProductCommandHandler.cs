using Common.Dtos.Response;
using Common.Enums;
using ErrorOr;
using MediatR;
using ProductService.Domain.Entity;
using ProductService.Domain.Errors;
using ProductService.Infrastructure.Data;

namespace ProductService.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Product>>
{
    private readonly ProductDbContext _context;

    public UpdateProductCommandHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id, cancellationToken);

        if (product == null)
        {
            return Errors.ProductNotFound;
        }
        
        product.Name = request.ProductDto.Name;
        product.Description = request.ProductDto.Description;
        product.Price = request.ProductDto.Price;
        product.Category = Enum.Parse<Category>(request.ProductDto.Category, true);
        product.InStock = request.ProductDto.InStock;
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }
}