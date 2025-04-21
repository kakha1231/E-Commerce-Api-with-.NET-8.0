using Common.Dtos.Response;
using ErrorOr;
using MediatR;
using ProductService.Domain.Errors;
using ProductService.Domain.Models;
using ProductService.Infrastructure.Entity;

namespace ProductService.Application.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand,ErrorOr<string>>
{
    private readonly ProductDbContext _context;

    public DeleteProductCommandHandler(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id, cancellationToken);
      
        if (product == null)
        {
            return Errors.ProductNotFound;
        }
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return "Product Deleted";
    }
}