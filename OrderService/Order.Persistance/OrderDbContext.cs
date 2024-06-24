using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Persistance;

internal class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>().ToTable("orders");

        modelBuilder.Entity<OrderEntity>()
            .Property(o => o.currency)
            .HasConversion<string>();

        modelBuilder.Entity<OrderEntity>()
            .Property(o => o.status)
            .HasConversion<string>();
    }
}