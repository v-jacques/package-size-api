using Microsoft.EntityFrameworkCore;
using PackageSize.Domain;
using System;

namespace PackageSize.Web.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Auto-increments OrderItem.OrderItemID
            modelBuilder
                .Entity<OrderItem>()
                .Property(oi => oi.OrderItemID)
                .ValueGeneratedOnAdd();

            // Relationship between Order and OrderItem
            modelBuilder
                .Entity<Order>()
                .HasMany<OrderItem>(o => o.OrderItems)
                .WithOne();
        }
    }
}
