using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Agregates;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Order>()
            .HasMany(order => order.Items)
            .WithOne(orderItem => orderItem.Order)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Order>()
            .ComplexProperty(o => o.Shipping, shipping => // Configure ShippingInfo as an owned entity
            {
                shipping.Property(s => s.ContactName).IsRequired();
                shipping.Property(s => s.Phone).IsRequired();
                shipping.Property(s => s.Address).IsRequired();
                shipping.Property(s => s.City).IsRequired();
                shipping.Property(s => s.State).IsRequired();
                shipping.Property(s => s.ZipCode).IsRequired();
                shipping.Property(s => s.Country).IsRequired();
                shipping.Property(s => s.Courier).IsRequired();
            });
        
    }
    
}