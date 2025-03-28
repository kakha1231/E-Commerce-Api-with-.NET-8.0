using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Agregates;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ShippingInfo> ShippingInfos { get; set; }
    
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
            .HasOne(order => order.Shipping)
            .WithOne(shipping => shipping.Order)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}