using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entity;

namespace ProductService.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
        
    }
    
}