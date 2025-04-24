using Common.Enums;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entity;

namespace ProductService.Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetProductById(int id)
    {
        var product = await _context.Products.FindAsync(id);

        return product;
    }

    public async Task<List<Product>> GetProducts(string? category, string? searchString, decimal? minPrice = 0M,
        decimal? maxPrice = 9999999M, int page = 1, int pageSize = 20)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(p => p.Name.Contains(searchString));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            if (Enum.TryParse<Category>(category, true, out var parsedCategory))
            {
                query = query.Where(p => p.Category == parsedCategory);
            }
            else
            {
                return []; 
            }
        }

        query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.InStock);

        var products = await query
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return products;
    }

    public async Task CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}