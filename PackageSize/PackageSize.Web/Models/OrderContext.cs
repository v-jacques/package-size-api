using Microsoft.EntityFrameworkCore;
using PackageSize.Domain;

namespace PackageSize.Web.Models
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

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

            //modelBuilder
            //    .Entity<OrderItem>()
            //    .Property(oi => oi.ProductType)
            //    .HasConversion(
            //        v => v.ToString(),
            //        v => (ProductType)Enum.Parse(typeof(ProductType), v));

            //modelBuilder
            //    .Entity<Order>()
            //    .HasMany<OrderItem>(o => o.OrderItems)
            //    .WithOne();
        }
    }
}
